using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Implementations
{
    public class VerificacaoEmprestimoRepository : BaseRepository<VerificacaoEmprestimo>, IVerificacaoEmprestimoRepository
    {
        public VerificacaoEmprestimoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
