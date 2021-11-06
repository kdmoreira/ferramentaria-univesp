using AutoMapper;
using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.OperationResponses;
using Domain.Security;
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
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public EmprestimoService(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task EmprestarAsync(EmprestimoCriacaoDTO dto, Guid usuarioLogadoID)
        {
            var emprestimo = _mapper.Map<Emprestimo>(dto);

            await ValidacaoSenhaAsync(dto);
            var roleUsuario = await RoleUsuarioLogadoAsync(usuarioLogadoID);

            emprestimo.Realizar(dto.PrazoEmDias);

            await ValidacaoAsync(emprestimo);

            var ferramenta = await _unitOfWork.FerramentaRepository.FindByAsync(x => x.ID == emprestimo.FerramentaID);
            ferramenta.Emprestar(emprestimo.Quantidade);
            _unitOfWork.FerramentaRepository.Update(ferramenta, x => x.ID == ferramenta.ID, usuarioLogadoID);

            await _unitOfWork.EmprestimoRepository.AddAsync(emprestimo, usuarioLogadoID);
            await _unitOfWork.CommitAsync();

            var colaborador = _unitOfWork.ColaboradorRepository.FindBy(x => x.ID == emprestimo.ColaboradorID);
            await _emailSender.EnviarEmailEmprestimoRealizadoAsync(colaborador, ferramenta, emprestimo);

            if (roleUsuario != RoleEnum.Administrador)
            {
                await NotificarAdministradores(emprestimo, ferramenta, colaborador);
            }
        }        

        public async Task DevolverAsync(Guid id, Guid usuarioLogadoID)
        {
            var emprestimo = await _unitOfWork.EmprestimoRepository.FindByAsync(x => x.ID == id,
                include: x => x
                .Include(x => x.Ferramenta));

            emprestimo.Finalizar();
            _unitOfWork.EmprestimoRepository.Update(emprestimo, x => x.ID == emprestimo.ID, usuarioLogadoID);

            var ferramenta = emprestimo.Ferramenta;
            ferramenta.Devolver(emprestimo.Quantidade);
            _unitOfWork.FerramentaRepository.Update(ferramenta, x => x.ID == ferramenta.ID, usuarioLogadoID);

            await _unitOfWork.CommitAsync();
        }

        public async Task<EmprestimoDTO> BuscarPorIDAsync(Guid id)
        {
            var emprestimo = await _unitOfWork.EmprestimoRepository.FindByAsync(x => x.ID == id,
                include: x => x
                .Include(x => x.Ferramenta)
                .Include(x => x.Colaborador));

            if (emprestimo == null)
                throw new InvalidOperationException("Empréstimo não encontrado.");

            var retorno = _mapper.Map<EmprestimoDTO>(emprestimo);
            return retorno;
        }

        public async Task<ListagemResponse<EmprestimoPorColaboradorDTO>> BuscarPorColaboradorAsync(Guid colaboradorID, Guid usuarioLogadoID)
        {
            await ValidarUsuarioLogadoAsync(colaboradorID, usuarioLogadoID);

            var retorno = new ListagemResponse<EmprestimoPorColaboradorDTO>();

            var resultados = await _unitOfWork.EmprestimoRepository.ListByAsync(x => x.Ativo == true && x.ColaboradorID == colaboradorID,
                include: x => x.Include(x => x.Ferramenta));

            var listagem = _mapper.Map<List<EmprestimoPorColaboradorDTO>>(resultados);
            retorno.Data = listagem;
            retorno.Count = resultados.Count;

            return retorno;
        }        

        public async Task<ListagemResponse<EmprestimoListagemDTO>> ListaPaginadaAsync(string ferramenta, 
            string colaborador, int numeroPagina, int tamanhoPagina, Guid usuarioLogadoID)
        {
            await VerificacaoStatusEmprestimosAsync(usuarioLogadoID);

            var retorno = new ListagemResponse<EmprestimoListagemDTO>();

            var totalRegistros = 0;

            Expression<Func<Emprestimo, bool>> exp = x =>
            x.Ativo == true
            && (string.IsNullOrEmpty(ferramenta) ? true : x.Ferramenta.Descricao == ferramenta)
            && (string.IsNullOrEmpty(colaborador) ? true : 
            x.Colaborador.Nome.Contains(colaborador) || x.Colaborador.Sobrenome.Contains(colaborador));

            var resultados = _unitOfWork.EmprestimoRepository.ListByPaged(exp, numeroPagina, tamanhoPagina,
                out totalRegistros,                
                o => o.OrderBy(o => o.DataDevolucao),
                include: x => x
                .Include(x => x.Colaborador)
                .Include(x => x.Ferramenta));

            var listagem = _mapper.Map<List<EmprestimoListagemDTO>>(resultados);
            retorno.Data = listagem;
            retorno.Count = totalRegistros;

            return retorno;
        }

        // Private Methods
        private async Task ValidarUsuarioLogadoAsync(Guid colaboradorID, Guid usuarioLogadoID)
        {
            var usuario = await _unitOfWork.UsuarioRepository.FindByAsync(x => x.ColaboradorID == colaboradorID);

            if (usuario.ID != usuarioLogadoID)
                if (usuario.Role != RoleEnum.Administrador)
                    throw new InvalidOperationException("Você não tem permissão para visualizar esta página.");
        }

        private async Task VerificacaoStatusEmprestimosAsync(Guid usuarioLogadoID)
        {
            var verificacao = _unitOfWork.VerificacaoEmprestimoRepository.ListAll().SingleOrDefault();
            var dataUltima = verificacao.UltimaVerificacao;
            var dataAtual = DateTime.UtcNow.Date;

            if (dataUltima < dataAtual)
            {
                var atrasados = new List<Emprestimo>();
                var emprestimosEmAndamento = await _unitOfWork.EmprestimoRepository.ListByAsync(x => x.Status == StatusEmprestimoEnum.EmDia);
                foreach (var emprestimo in emprestimosEmAndamento)
                {
                    if (emprestimo.VerificarAtraso(dataAtual))
                        atrasados.Add(emprestimo);                                            
                }
                
                if (atrasados.Any())
                {
                    _unitOfWork.EmprestimoRepository.UpdateMany(atrasados);
                    await NotificarAtrasosAsync(atrasados);
                }

                verificacao.Atualizar(dataAtual);
                _unitOfWork.VerificacaoEmprestimoRepository.Update(verificacao, x => x.ID == verificacao.ID, usuarioLogadoID);

                await _unitOfWork.CommitAsync();
            }            
        }

        private async Task NotificarAtrasosAsync(IList<Emprestimo> atrasados)
        {
            var colaboradoresIDs = atrasados.Select(x => x.ColaboradorID);
            var ferramentasIDs = atrasados.Select(x => x.FerramentaID);
            var colaboradores = await _unitOfWork.ColaboradorRepository.ListByAsync(x => colaboradoresIDs.Contains(x.ID));
            var ferramentas = await _unitOfWork.FerramentaRepository.ListByAsync(x => ferramentasIDs.Contains(x.ID));
            foreach (var atraso in atrasados)
            {
                var colaborador = colaboradores.FirstOrDefault(x => x.ID == atraso.ColaboradorID);
                var ferramenta = ferramentas.FirstOrDefault(x => x.ID == atraso.FerramentaID);
                await _emailSender.EnviarEmailEmprestimoAtrasadoAsync(colaborador, ferramenta, atraso);
            }
        }

        private async Task ValidacaoAsync(Emprestimo emprestimo)
        {
            var validationResult = new EmprestimoValidator().Validate(emprestimo);

            if (!validationResult.IsValid)
                throw new InvalidOperationException(string.Join("\n", validationResult.Errors.Select(x => x)));

            var testeExistencia = false;

            testeExistencia = await _unitOfWork.ColaboradorRepository.AnyAsync(x => x.ID == emprestimo.ColaboradorID && x.Ativo == true);

            if (!testeExistencia)
                throw new InvalidOperationException("Colaborador inválido.");

            testeExistencia = await _unitOfWork.FerramentaRepository.AnyAsync(x => 
            x.ID == emprestimo.FerramentaID && x.Ativo == true && x.Status == StatusFerramentaEnum.Disponivel);
            
            if (!testeExistencia)
                throw new InvalidOperationException("Ferramenta inválida.");
        }

        private async Task ValidacaoSenhaAsync(EmprestimoCriacaoDTO dto)
        {
            var usuario = await _unitOfWork.UsuarioRepository.FindByAsync(x => x.ColaboradorID == dto.ColaboradorID);
            var matchSenha = PasswordUtil.VerificarSenha(dto.SenhaColaborador, usuario.Senha);
            if (matchSenha == false)
                throw new InvalidOperationException("Senha inválida para realizar o empréstimo.");
        }

        private async Task<RoleEnum> RoleUsuarioLogadoAsync(Guid usuarioLogadoID)
        {
            var usuario = await _unitOfWork.UsuarioRepository.FindByAsync(x => x.ID == usuarioLogadoID);
            return usuario.Role;
        }

        private async Task NotificarAdministradores(Emprestimo emprestimo, Ferramenta ferramenta, Colaborador colaborador)
        {
            var usuariosAdmins = await _unitOfWork.UsuarioRepository
                                .ListByAsync(x => x.Role == RoleEnum.Administrador,
                                include: x => x.Include(x => x.Colaborador));

            var admins = usuariosAdmins.Select(x => x.Colaborador);

            foreach (var admin in admins)
            {
                await _emailSender.EnviarEmailEmprestimoPorColaboradorAsync(colaborador, ferramenta, emprestimo, admin);
            }
        }
    }
}