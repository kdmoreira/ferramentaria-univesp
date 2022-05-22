using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Usuario;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Security;
using Microsoft.EntityFrameworkCore;
using Service.EmailService.Interfaces;
using Service.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
        {
            return await ValidacaoLoginAsync(dto);
        }

        public async Task<string> RecuperarSenhaAsync(RecuperarSenhaDTO dto)
        {
            var usuario = await _unitOfWork.UsuarioRepository.FindByAsync(x => x.Login == dto.Login,
                include: x => x.Include(x => x.Colaborador));
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            var colaborador = usuario.Colaborador;

            var token = usuario.TokenRecuperacaoSenha();

            _unitOfWork.UsuarioRepository.Update(usuario, x => x.ID == usuario.ID, usuario.ID);
            await _unitOfWork.CommitAsync();

            await _emailSender.EnviarEmailRecuperacaoSenhaAsync(colaborador, token);

            var email = usuario.Colaborador.Email;
            var emailOcultado = string.Format("{0}****{1}", email[0], email.Substring(email.IndexOf('@') - 1));
            var response = $"Um e-mail de recuperação de senha foi enviado para {emailOcultado}.";
            return response;
        }

        public async Task AlterarSenhaAsync(AlterarSenhaDTO dto)
        {
            var usuario = await _unitOfWork.UsuarioRepository.FindByAsync(x =>
            x.Login == dto.Login && x.Token == dto.Token);

            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            TrocarSenha(dto, usuario);
            await _unitOfWork.CommitAsync();
        }

        public async Task PrimeiroAcessoAsync(NovaSenhaDTO dto)
        {
            var usuario = await _unitOfWork.UsuarioRepository.FindByAsync(x => x.Token == dto.Token);
            if (usuario == null)
                throw new InvalidOperationException("O token informado é inválido!");

            TrocarSenha(dto, usuario);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Usuario> AdicionarAsync(ColaboradorCriacaoDTO dto, Colaborador colaborador, Guid usuarioLogadoID)
        {
            var usuario = new Usuario();
            usuario.Cadastrar(colaborador.Matricula, colaborador.ID, dto.Role);
            await _unitOfWork.UsuarioRepository.AddAsync(usuario, usuarioLogadoID);
            return usuario;
        }

        public void Alterar(ColaboradorCriacaoDTO dto, Colaborador colaborador, Colaborador antigo, Guid usuarioLogadoID)
        {
            var usuario = antigo.Usuario;
            usuario.EquipararPropriedades(colaborador.Matricula, dto.Role);

            _unitOfWork.UsuarioRepository.Update(usuario, x => x.ID == usuario.ID, usuarioLogadoID);
        }

        public void Ativar(Colaborador colaborador, Guid usuarioLogadoID)
        {
            var usuario = colaborador.Usuario;
            usuario.Ativar();

            _unitOfWork.UsuarioRepository.Update(usuario, x => x.ID == usuario.ID, usuarioLogadoID);
        }

        public void Inativar(Colaborador colaborador, Guid usuarioLogadoID)
        {
            var usuario = colaborador.Usuario;
            usuario.Inativar();

            _unitOfWork.UsuarioRepository.Update(usuario, x => x.ID == usuario.ID, usuarioLogadoID);
        }

        // Private Methods
        private void ValidacaoAsync(NovaSenhaDTO dto)
        {
            var validationResult = new NovaSenhaValidator().Validate(dto);

            if (!validationResult.IsValid)
                throw new InvalidOperationException(string.Join("\n", validationResult.Errors.Select(x => x)));
        }

        private async Task<LoginResponseDTO> ValidacaoLoginAsync(LoginDTO dto)
        {
            var response = new LoginResponseDTO()
            {
                Autenticado = false
            };

            if (string.IsNullOrEmpty(dto.Login))
            {
                response.Mensagem = "Login em branco. Informar login.";
                return response;
            }

            if (string.IsNullOrEmpty(dto.Senha))
            {
                response.Mensagem = "Senha em branco. Informar senha.";
                return response;
            }

            var usuario = await _unitOfWork.UsuarioRepository
               .FindByAsync(x => x.Login == dto.Login);

            var matchSenha = false;
            if (usuario != null)
                matchSenha = PasswordUtil.VerificarSenha(dto.Senha, usuario.Senha);

            
            if (matchSenha == false)
            {
                response.Mensagem = "O usuário não existe ou a senha está incorreta.";
                return response;
            }

            if (usuario.Ativo == false)
            {
                response.Mensagem = "O usuário não está ativo.";
                return response;
            }

            response.Autenticado = true;
            response.Token = usuario.TokenAcesso();
            return response;
        }

        private void TrocarSenha(NovaSenhaDTO dto, Usuario usuario)
        {
            ValidacaoAsync(dto);

            usuario.TrocarSenha(dto.SenhaNova);

            _unitOfWork.UsuarioRepository.Update(usuario, x => x.ID == usuario.ID, usuario.ID);
        }
    }
}
