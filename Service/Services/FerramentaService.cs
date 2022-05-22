using AutoMapper;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.OperationResponses;
using Microsoft.EntityFrameworkCore;
using Service.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FerramentaService : IFerramentaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FerramentaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AdicionarAsync(FerramentaCriacaoDTO dto, Guid usuarioLogadoID)
        {
            var ferramenta = _mapper.Map<Ferramenta>(dto);
            ferramenta.Cadastrar();

            await ValidacaoAsync(ferramenta, true);

            await _unitOfWork.FerramentaRepository.AddAsync(ferramenta, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public async Task AtualizarAsync(FerramentaEdicaoDTO dto, Guid usuarioLogadoID)
        {
            var antiga = await _unitOfWork.FerramentaRepository.FindByAsync(x => x.ID == dto.ID);
            if (antiga == null)
                throw new InvalidOperationException("Ferramenta não encontrada para edição.");

            var ferramenta = _mapper.Map<Ferramenta>(dto);
            ferramenta.RecuperarPropriedades(antiga);

            await ValidacaoAsync(ferramenta, false);

            _unitOfWork.FerramentaRepository.Update(ferramenta, x => x.ID == ferramenta.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public async Task<FerramentaDTO> BuscarPorIDAsync(Guid id)
        {
            var ferramenta = await _unitOfWork.FerramentaRepository.FindByAsync(x => x.ID == id,
                include: x => x.Include(x => x.Categoria));

            if (ferramenta == null)
                throw new InvalidOperationException("Ferramenta não encontrada");

            var retorno = _mapper.Map<FerramentaDTO>(ferramenta);
            return retorno;
        }

        public async Task AtivarAsync(Guid id, Guid usuarioLogadoID)
        {
            var ferramenta = await _unitOfWork.FerramentaRepository
                .FindByAsync(x => x.ID == id && x.Ativo == false);

            if (ferramenta == null)
                throw new InvalidOperationException("Ferramenta inválida para ativar.");

            ferramenta.Ativar();
            _unitOfWork.FerramentaRepository.Update(ferramenta, x => x.ID == ferramenta.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public async Task InativarAsync(Guid id, Guid usuarioLogadoID)
        {
            var ferramenta = await _unitOfWork.FerramentaRepository
                .FindByAsync(x => x.ID == id && x.Ativo == true);

            if (ferramenta == null)
                throw new InvalidOperationException("Ferramenta inválida para inativar.");

            ferramenta.Inativar();
            _unitOfWork.FerramentaRepository.Update(ferramenta, x => x.ID == ferramenta.ID, usuarioLogadoID);
            await _unitOfWork.CommitAsync();
        }

        public ListagemResponse<FerramentaListagemDTO> ListaPaginada(string codigo, string descricao, int numeroPagina, int tamanhoPagina)
        {
            var retorno = new ListagemResponse<FerramentaListagemDTO>();

            var totalRegistros = 0;

            Expression<Func<Ferramenta, bool>> exp = x =>
            x.Ativo == true
            && (string.IsNullOrEmpty(codigo) ? true : x.Codigo == codigo)
            && (string.IsNullOrEmpty(descricao) ? true : x.Descricao.Contains(descricao));

            var resultados = _unitOfWork.FerramentaRepository.ListByPaged(exp, numeroPagina, tamanhoPagina,
                out totalRegistros,
                o => o.OrderBy(o => o.Descricao));

            var listagem = _mapper.Map<List<FerramentaListagemDTO>>(resultados);
            retorno.Data = listagem;
            retorno.Count = totalRegistros;

            return retorno;
        }

        public async Task<ListagemResponse<CategoriaDTO>> BuscarCategoriasAsync()
        {
            var retorno = new ListagemResponse<CategoriaDTO>();

            var categorias = await _unitOfWork.CategoriaRepository.ListAllAsync();

            var listagem = _mapper.Map<List<CategoriaDTO>>(categorias);
            retorno.Data = listagem;
            retorno.Count = listagem == null ? 0 : listagem.Count;

            return retorno;
        }

        // Private Methods
        private async Task ValidacaoAsync(Ferramenta ferramenta, bool novo)
        {
            var validationResult = new FerramentaValidator().Validate(ferramenta);

            if (!validationResult.IsValid)
                throw new InvalidOperationException(string.Join("\n", validationResult.Errors.Select(x => x)));

            var testeExistencia = false;

            if (novo)
                testeExistencia = await _unitOfWork.FerramentaRepository.AnyAsync(x => x.Codigo == ferramenta.Codigo);
            else
                testeExistencia = await _unitOfWork.FerramentaRepository
                    .AnyAsync(x => x.Codigo == ferramenta.Codigo && x.ID != ferramenta.ID);

            if (testeExistencia)
                throw new InvalidOperationException("Já existe uma ferramenta com este código.");

            if (novo)
                testeExistencia = await _unitOfWork.FerramentaRepository.AnyAsync(x => x.NumeroPatrimonial == ferramenta.NumeroPatrimonial);
            else
                testeExistencia = await _unitOfWork.FerramentaRepository
                    .AnyAsync(x => x.NumeroPatrimonial == ferramenta.NumeroPatrimonial && x.ID != ferramenta.ID);

            if (testeExistencia)
                throw new InvalidOperationException("Já existe uma ferramenta com este número patrimonial.");
        }
    }
}
