using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Implementations
{
    public class ColaboradorRepository : BaseRepository<Colaborador>, IColaboradorRepository
    {
        public ColaboradorRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddManyAsync(IList<Colaborador> colaboradores, Guid usuarioLogadoId)
        {
            await _dbContext.AddRangeAsync(colaboradores, usuarioLogadoId);
        }
    }
}