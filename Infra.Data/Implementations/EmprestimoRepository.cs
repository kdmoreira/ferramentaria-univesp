using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Implementations
{
    public class EmprestimoRepository : BaseRepository<Emprestimo>, IEmprestimoRepository
    {
        public EmprestimoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}