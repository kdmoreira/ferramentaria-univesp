using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infra.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public IFerramentaRepository FerramentaRepository { get; set; }
        public IColaboradorRepository ColaboradorRepository { get; set; }
        public IEmprestimoRepository EmprestimoRepository { get; set; }
        public IUsuarioRepository UsuarioRepository { get; set; }
        public ICategoriaRepository CategoriaRepository { get; set; }
        public IVerificacaoEmprestimoRepository VerificacaoEmprestimoRepository { get; set; }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;

            FerramentaRepository = new FerramentaRepository(_dbContext);
            ColaboradorRepository = new ColaboradorRepository(_dbContext);
            EmprestimoRepository = new EmprestimoRepository(_dbContext);
            UsuarioRepository = new UsuarioRepository(_dbContext);
            CategoriaRepository = new CategoriaRepository(_dbContext);
            VerificacaoEmprestimoRepository = new VerificacaoEmprestimoRepository(_dbContext);
        }

        public async Task<bool> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}