using AutoMapper;
using Domain.DTOs;
using Domain.Enums;
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
        private readonly IUsuarioService _usuarioService;

        public ColaboradorService(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender, IUsuarioService usuarioService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            _usuarioService = usuarioService;
        }

        public async Task AdicionarAsync(ColaboradorCriacaoDTO dto, Guid usuarioLogadoID)
        {
            var colaborador = _mapper.Map<Colaborador>(dto);            

            await ValidacaoAsync(colaborador, true);

            await _unitOfWork.ColaboradorRepository.AddAsync(colaborador, usuarioLogadoID);
            var usuario = await _usuarioService.AdicionarAsync(dto, colaborador, usuarioLogadoID);
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

            _usuarioService.Alterar(dto, colaborador, antigo, usuarioLogadoID);            

            _unitOfWork.ColaboradorRepository.Update(colaborador, x => x.ID == colaborador.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ColaboradorDTO> BuscarPorIDAsync(Guid id, Guid usuarioLogadoID)
        {
            await ValidarUsuarioLogadoAsync(id, usuarioLogadoID);

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
                .FindByAsync(x => x.ID == id && x.Ativo == false,
                include: x => x.Include(x => x.Usuario));

            if (colaborador == null)
                throw new InvalidOperationException("Colaborador inválido para ativar.");

            colaborador.Ativar();
            _usuarioService.Ativar(colaborador, usuarioLogadoID);

            _unitOfWork.ColaboradorRepository.Update(colaborador, x => x.ID == colaborador.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public async Task InativarAsync(Guid id, Guid usuarioLogadoID)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository
                .FindByAsync(x => x.ID == id && x.Ativo == true,
                include: x => x.Include(x => x.Usuario));

            if (colaborador == null)
                throw new InvalidOperationException("Colaborador inválido para inativar.");

            colaborador.Inativar();
            _usuarioService.Inativar(colaborador, usuarioLogadoID);

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
                .ThenBy(o => o.Sobrenome),
                include: x => x.Include(x => x.Emprestimos.Where(x => x.Ativo == true)));

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

        private async Task ValidarUsuarioLogadoAsync(Guid colaboradorID, Guid usuarioLogadoID)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.FindByAsync(x => x.ID == colaboradorID,
                            include: x => x.Include(x => x.Usuario));

            if (colaboradorID != usuarioLogadoID)
                if (colaborador.Usuario.Role != RoleEnum.Administrador)
                    throw new InvalidOperationException("Você não tem permissão para visualizar esta página.");
        }
    }
}
