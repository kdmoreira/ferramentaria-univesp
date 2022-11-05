using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IEmprestimoRepository : IRepository<Emprestimo>, IAsyncRepository<Emprestimo>
    {
        void UpdateMany(IList<Emprestimo> emprestimos);
        Task AddManyAsync(IList<Emprestimo> emprestimos, Guid usuarioLogadoId);
    }
}
