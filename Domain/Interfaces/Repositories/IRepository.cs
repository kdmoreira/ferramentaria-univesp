using Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        T GetById(Guid id);
        IEnumerable<T> ListAll();
        T FindBy(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null);
        void Add(T model, Guid usuarioLogadoId);
        void Update(T model, Expression<Func<T, bool>> where, Guid usuarioLogadoId);
        void Delete(T model);
        IEnumerable<T> ListBy(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null);
        bool Commit();
        List<T> ListByPaged(Expression<Func<T, bool>> where, int pageNumber, int pageSize, out int count, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null);
    }
}
