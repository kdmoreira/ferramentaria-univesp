using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IFerramentaRepository : IRepository<Ferramenta>, IAsyncRepository<Ferramenta>
    {
        Task AddManyAsync(IList<Ferramenta> ferramentas, Guid usuarioLogadoId);
    }
}