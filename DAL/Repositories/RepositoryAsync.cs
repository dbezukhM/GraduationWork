﻿using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class RepositoryAsync<T, TKey> : IRepositoryAsync<T, TKey>
        where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public RepositoryAsync(DbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T> GetAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task<T> GetWithDetailsAsync(Guid key, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return await  query.FirstOrDefaultAsync(x => x.Id == key);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsQueryable().FirstOrDefaultAsync(predicate);
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsQueryable().Where(predicate).ToArrayAsync();
        }

        public async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return await query.ToArrayAsync();
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet;
            return query.AnyAsync(predicate);
        }

        public async Task<uint> CountAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet;
            var count = await query.CountAsync(predicate);
            return (uint)count;
        }

        public async ValueTask<T> AddAsync(T entity)
        {
            var a = await _dbSet.AddAsync(entity);
            return a.Entity;
        }

        public Task<ICollection<T>> AddAsync(IEnumerable<T> entities)
        {
            var entityArr = entities.ToList();
            if (!entityArr.Any())
            {
                return EmptyCollection();
            }

            return _dbSet.AddRangeAsync(entityArr)
                .ContinueWith(_ => (ICollection<T>)entityArr);
        }

        public T Update(T entity) => _dbSet.Update(entity).Entity;

        public ICollection<T> UpdateAsync(IEnumerable<T> entities)
        {
            var entityArr = entities.ToArray();
            if (!entityArr.Any())
            {
                return new List<T>();
            }

            _dbSet.UpdateRange(entityArr);
            return entityArr;
        }

        public T Delete(T entity) => _dbSet.Remove(entity).Entity;

        public void Delete(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public async ValueTask<T> DeleteAsync(TKey key)
        {
            var entityToDelete = await GetAsync(key);
            return Delete(entityToDelete);
        }

        public async Task<ICollection<T>> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entitiesToDelete = await FindAllAsync(predicate);

            if (!entitiesToDelete.Any())
            {
                return new List<T>();
            }

            _dbSet.RemoveRange(entitiesToDelete);
            return entitiesToDelete;
        }

        private static Task<ICollection<T>> EmptyCollection()
        {
            return Task.FromResult((ICollection<T>)new List<T>());
        }
    }

    public class EpRepositoryAsync<T> : RepositoryAsync<T, Guid>, IEpRepositoryAsync<T>
        where T : BaseEntity
    {
        public EpRepositoryAsync(EducationalProgramsDbContext dbContext)
            : base(dbContext)
        {
        }
    }

    public class WpRepositoryAsync<T> : RepositoryAsync<T, Guid>, IWpRepositoryAsync<T>
        where T : BaseEntity
    {
        public WpRepositoryAsync(WorkingProgramsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}