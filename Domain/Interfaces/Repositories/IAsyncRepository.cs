using Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IAsyncRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> ListAllAsync();
        Task<T> FindByAsync(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null);
        Task<List<T>> ListByAsync(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null);
        Task AddAsync(T model, Guid usuarioLogadoId);
        Task<bool> CommitAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);        
    }
}