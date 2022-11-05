using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Implementations
{
    public class EmprestimoRepository : BaseRepository<Emprestimo>, IEmprestimoRepository
    {
        public EmprestimoRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddManyAsync(IList<Emprestimo> emprestimos, Guid usuarioLogadoId)
        {
            await _dbContext.AddRangeAsync(emprestimos, usuarioLogadoId);
        }

        public void UpdateMany(IList<Emprestimo> emprestimos)
        {
            _dbContext.Set<Emprestimo>().UpdateRange(emprestimos);
        }
    }
}