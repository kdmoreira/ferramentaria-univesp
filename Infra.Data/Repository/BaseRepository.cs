using Domain.Audits;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class BaseRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseModel
    {

        protected readonly DbContext _dbContext;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T model, Guid usuarioLogadoId)
        {
            Audit.Creation(model, usuarioLogadoId);
            _dbContext.Set<T>().AddAsync(model);
        }

        public async Task AddAsync(T model, Guid usuarioLogadoId)
        {
            Audit.Creation(model, usuarioLogadoId);
            await _dbContext.Set<T>().AddAsync(model);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            return await query.Where(where).AnyAsync();
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Delete(T model)
        {
            _dbContext.Set<T>().Remove(model);
        }

        public T FindBy(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null)
        {
            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            if (include != null)
            {
                query = include.Compile()(query);
            }

            return query.Where(where).FirstOrDefault();
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null)
        {
            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            if (include != null)
            {
                query = include.Compile()(query);
            }

            return await query.Where(where).FirstOrDefaultAsync();
        }

        public T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsNoTracking().AsEnumerable();
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public IEnumerable<T> ListBy(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null)
        {
            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            if (include != null)
            {
                query = include.Compile()(query);
            }

            return query.Where(where).AsNoTracking();
        }

        public async Task<List<T>> ListByAsync(Expression<Func<T, bool>> where, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null)
        {
            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            if (include != null)
            {
                query = include.Compile()(query);
            }

            return await query.Where(where).AsNoTracking().ToListAsync();
        }

        public List<T> ListByPaged(Expression<Func<T, bool>> where, int pageNumber, int pageSize, out int count
            , Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy
            , Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null)
        {
            count = 0;
            if (pageNumber < 0)
                pageNumber = 0;
            var skip = pageNumber * pageSize;

            if (pageSize <= 0)
                pageSize = 1;

            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            if (include != null)
            {
                query = include.Compile()(query);
            }

            if (where != null)
            {
                query = query.Where(where).AsNoTracking();
            }

            count = query.Count();

            return orderBy.Compile()(query).Skip(skip).Take(pageSize).ToList();
        }

        public void Update(T model, Expression<Func<T, bool>> where, Guid usuarioLogadoId)
        {
            var result = _dbContext.Set<T>().Single(where);
            if (result == null)
            {
                throw new Exception("Entidade não encontrada para atualização");
            }
            Audit.UpdateProperties(model, result, usuarioLogadoId);
            _dbContext.Entry(result).CurrentValues.SetValues(model);
        }
    }
}
