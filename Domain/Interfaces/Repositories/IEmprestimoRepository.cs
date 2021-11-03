using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface IEmprestimoRepository : IRepository<Emprestimo>, IAsyncRepository<Emprestimo>
    {
        void UpdateMany(IList<Emprestimo> emprestimos);
    }
}
