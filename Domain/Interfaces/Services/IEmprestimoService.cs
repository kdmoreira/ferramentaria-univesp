using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IEmprestimoService
    {
        List<EmprestimoListagemDTO> ListaPaginada(int numeroPagina, int tamanhoPagina);
        EmprestimoDTO BuscaPorID(Guid id);
        Task<bool> EmprestarAsync(EmprestimoCriacaoDTO dto, Guid usuarioLogadoID);
    }
}
