using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>, IAsyncRepository<Usuario>
    {
    }
}
