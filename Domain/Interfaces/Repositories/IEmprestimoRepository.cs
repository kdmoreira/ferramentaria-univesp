using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IEmprestimoRepository : IRepository<Emprestimo>, IAsyncRepository<Emprestimo>
    {

    }
}
