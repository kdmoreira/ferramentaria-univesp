using Domain.DTOs.AnaliseDados;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAnaliseDadosService
    {
        Task GerarMassaDados();
        Task<RelatorioFerramentasDTO> GerarRelatorioFerramentas();
        Task<RelatorioEmprestimosDTO> GerarRelatorioEmprestimos();
    }
}
