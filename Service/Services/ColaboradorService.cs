using AutoMapper;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.OperationResponses;
using Microsoft.EntityFrameworkCore;
using Service.EmailService.Interfaces;
using Service.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public ColaboradorService(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task AdicionarAsync(ColaboradorCriacaoDTO dto, Guid usuarioLogadoID)
        {
            var colaborador = _mapper.Map<Colaborador>(dto);            

            await ValidacaoAsync(colaborador, true);

            await _unitOfWork.ColaboradorRepository.AddAsync(colaborador, usuarioLogadoID);
            var usuario = await CriarUsuarioAsync(dto, colaborador, usuarioLogadoID);
            await _unitOfWork.CommitAsync();

            await _emailSender.EnviarEmailPrimeiroAcessoAsync(colaborador, usuario.Token);
        }

        public async Task AtualizarAsync(ColaboradorEdicaoDTO dto, Guid usuarioLogadoID)
        {
            var antigo = await _unitOfWork.ColaboradorRepository.FindByAsync(x => x.ID == dto.ID, 
                include: x => x.Include(x => x.Usuario));
            if (antigo == null)
                throw new InvalidOperationException("Colaborador não encontrado para edição.");

            var colaborador = _mapper.Map<Colaborador>(dto);

            await ValidacaoAsync(colaborador, false);

            AlterarUsuario(dto, colaborador, antigo, usuarioLogadoID);            

            _unitOfWork.ColaboradorRepository.Update(colaborador, x => x.ID == colaborador.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ColaboradorDTO> BuscarPorIDAsync(Guid id)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.FindByAsync(x => x.ID == id,
                include: x => x
                .Include(x => x.Supervisor)
                .Include(x => x.Usuario));

            if (colaborador == null)
                throw new InvalidOperationException("Colaborador não encontrado.");

            var retorno = _mapper.Map<ColaboradorDTO>(colaborador);
            return retorno;
        }

        public async Task AtivarAsync(Guid id, Guid usuarioLogadoID)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository
                .FindByAsync(x => x.ID == id && x.Ativo == false);

            if (colaborador == null)
                throw new InvalidOperationException("Colaborador inválido para ativar.");

            colaborador.Ativar();
            _unitOfWork.ColaboradorRepository.Update(colaborador, x => x.ID == colaborador.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public async Task InativarAsync(Guid id, Guid usuarioLogadoID)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository
                .FindByAsync(x => x.ID == id && x.Ativo == true);

            if (colaborador == null)
                throw new InvalidOperationException("Colaborador inválido para inativar.");

            colaborador.Inativar();
            _unitOfWork.ColaboradorRepository.Update(colaborador, x => x.ID == colaborador.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public ListagemResponse<ColaboradorListagemDTO> ListaPaginada(string cpf, string matricula, string nome, int numeroPagina, int tamanhoPagina)
        {
            var retorno = new ListagemResponse<ColaboradorListagemDTO>();

            var totalRegistros = 0;

            Expression<Func<Colaborador, bool>> exp = x =>
            (string.IsNullOrEmpty(cpf) ? true : x.CPF == cpf)
            && (string.IsNullOrEmpty(matricula) ? true : x.Matricula == matricula)
            && (string.IsNullOrEmpty(nome) ? true : x.Nome.Contains(nome) || x.Sobrenome.Contains(nome));

            var resultados = _unitOfWork.ColaboradorRepository.ListByPaged(exp, numeroPagina, tamanhoPagina,
                out totalRegistros,
                o => o
                .OrderBy(o => o.Nome)
                .ThenBy(o => o.Sobrenome));

            var listagem = _mapper.Map<List<ColaboradorListagemDTO>>(resultados);
            retorno.Data = listagem;
            retorno.Count = totalRegistros;

            return retorno;
        }

        // Private Methods
        private async Task ValidacaoAsync(Colaborador colaborador, bool novo)
        {
            var validationResult = new ColaboradorValidator().Validate(colaborador);

            if (!validationResult.IsValid)
                throw new InvalidOperationException(string.Join("\n", validationResult.Errors.Select(x => x)));

            var testeExistencia = false;

            if (novo)
                testeExistencia = await _unitOfWork.ColaboradorRepository.AnyAsync(x => x.CPF == colaborador.CPF);
            else
                testeExistencia = await _unitOfWork.ColaboradorRepository
                    .AnyAsync(x => x.CPF == colaborador.CPF && x.ID != colaborador.ID);

            if (testeExistencia)
                throw new InvalidOperationException("Já existe uma colaborador com este CPF.");

            if (novo)
                testeExistencia = await _unitOfWork.ColaboradorRepository.AnyAsync(x => x.Matricula == colaborador.Matricula);
            else
                testeExistencia = await _unitOfWork.ColaboradorRepository
                    .AnyAsync(x => x.Matricula == colaborador.Matricula && x.ID != colaborador.ID);

            if (testeExistencia)
                throw new InvalidOperationException("Já existe uma colaborador com esta matrícula.");
        }

        private async Task<Usuario> CriarUsuarioAsync(ColaboradorCriacaoDTO dto, Colaborador colaborador, Guid usuarioLogadoID)
        {
            var usuario = new Usuario();
            usuario.Cadastrar(colaborador.CPF, colaborador.ID, dto.Role);
            await _unitOfWork.UsuarioRepository.AddAsync(usuario, usuarioLogadoID);
            return usuario;
        }

        private void AlterarUsuario(ColaboradorCriacaoDTO dto, Colaborador colaborador, Colaborador antigo, Guid usuarioLogadoID)
        {
            var usuario = antigo.Usuario;
            usuario.EquipararPropriedades(colaborador.CPF, dto.Role);            

            _unitOfWork.UsuarioRepository.Update(usuario, x => x.ID == usuario.ID, usuarioLogadoID);
        }
    }
}
