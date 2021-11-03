using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infra.Data.Implementations
{
    public class EmprestimoRepository : BaseRepository<Emprestimo>, IEmprestimoRepository
    {
        public EmprestimoRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void UpdateMany(IList<Emprestimo> emprestimos)
        {
            _dbContext.Set<Emprestimo>().UpdateRange(emprestimos);
        }
    }
}