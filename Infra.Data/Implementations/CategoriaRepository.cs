using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Implementations
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
