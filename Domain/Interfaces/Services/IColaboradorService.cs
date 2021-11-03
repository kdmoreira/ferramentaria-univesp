using Domain.DTOs;
using Domain.OperationResponses;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IColaboradorService
    {
        ListagemResponse<ColaboradorListagemDTO> ListaPaginada(string cpf, string matricula, string nome, int numeroPagina, int tamanhoPagina);
        Task<ColaboradorDTO> BuscarPorIDAsync(Guid id, Guid usuarioLogadoID);
        Task AdicionarAsync(ColaboradorCriacaoDTO dto, Guid usuarioLogadoID);
        Task AtualizarAsync(ColaboradorEdicaoDTO dto, Guid usuarioLogadoID);
        Task InativarAsync(Guid id, Guid usuarioLogadoID);
        Task AtivarAsync(Guid id, Guid usuarioLogadoID);
    }
}