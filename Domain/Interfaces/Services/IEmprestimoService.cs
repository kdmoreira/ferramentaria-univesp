using Domain.DTOs;
using Domain.OperationResponses;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IEmprestimoService
    {
        Task<ListagemResponse<EmprestimoListagemDTO>> ListaPaginadaAsync(string ferramenta, string colaborador, int numeroPagina, int tamanhoPagina, Guid usuarioLogadoID);
        Task<EmprestimoDTO> BuscarPorIDAsync(Guid id);
        Task EmprestarAsync(EmprestimoCriacaoDTO dto, Guid usuarioLogadoID);
        Task DevolverAsync(Guid id, Guid usuarioLogadoID);
        Task<ListagemResponse<EmprestimoPorColaboradorDTO>> BuscarPorColaboradorAsync(Guid colaboradorID, Guid usuarioLogadoID);
    }
}
