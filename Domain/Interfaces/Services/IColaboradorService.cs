using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IColaboradorService
    {
        List<ColaboradorListagemDTO> ListaPaginada(string cpf, string nome, int numeroPagina, int tamanhoPagina);
        ColaboradorDTO BuscaPorID(Guid id);
        Task<bool> AdicionarAsync(ColaboradorCriacaoDTO dto, Guid usuarioLogadoID);
        Task<bool> AtualizarAsync(ColaboradorEdicaoDTO dto, Guid usuarioLogadoID);
        Task<bool> InativarAsync(Guid usuarioLogadoID);
    }
}