using Domain.DTOs;
using Domain.OperationResponses;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IFerramentaService
    {
        ListagemResponse<FerramentaListagemDTO> ListaPaginada(string codigo, string nome, int numeroPagina, int tamanhoPagina);
        Task<FerramentaDTO> BuscarPorID(Guid id);
        Task AdicionarAsync(FerramentaCriacaoDTO dto, Guid usuarioLogadoID);
        Task AtualizarAsync(FerramentaEdicaoDTO dto, Guid usuarioLogadoID);
        Task InativarAsync(Guid id, Guid usuarioLogadoID);
        Task AtivarAsync(Guid id, Guid usuarioLogadoID);
    }
}
