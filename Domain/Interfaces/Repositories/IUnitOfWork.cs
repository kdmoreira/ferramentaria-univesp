using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IFerramentaRepository FerramentaRepository { get; }
        IColaboradorRepository ColaboradorRepository { get; }
        IEmprestimoRepository EmprestimoRepository { get; }
        Task<bool> CommitAsync();
    }
}
