using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Implementations
{
    public class FerramentaRepository : BaseRepository<Ferramenta>, IFerramentaRepository
    {
        public FerramentaRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddManyAsync(IList<Ferramenta> ferramentas, Guid usuarioLogadoId)
        {
            await _dbContext.AddRangeAsync(ferramentas, usuarioLogadoId);
        }
    }
}
