using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Implementations
{
    public class FerramentaRepository : BaseRepository<Ferramenta>, IFerramentaRepository
    {
        public FerramentaRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
