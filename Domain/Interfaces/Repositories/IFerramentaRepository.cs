using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IFerramentaRepository : IRepository<Ferramenta>, IAsyncRepository<Ferramenta>
    {

    }
}