using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IColaboradorRepository : IRepository<Colaborador>, IAsyncRepository<Colaborador>
    {
        Task AddManyAsync(IList<Colaborador> colaboradores, Guid usuarioLogadoId);
    }
}
