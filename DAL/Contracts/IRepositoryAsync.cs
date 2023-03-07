using System.Linq.Expressions;
using DAL.Entities;

namespace DAL.Contracts
{
    public interface IRepositoryAsync<T, in TKey>
        where T : BaseEntity
    {
        Task<T> GetAsync(TKey key);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> GetAllAsync();

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        Task<uint> CountAsync(Expression<Func<T, bool>> predicate);

        ValueTask<T> AddAsync(T entity);

        Task<ICollection<T>> AddAsync(IEnumerable<T> entity);

        T Update(T entity);

        ICollection<T> UpdateAsync(IEnumerable<T> entity);

        T Delete(T entity);

        void Delete(IEnumerable<T> entities);

        ValueTask<T> DeleteAsync(TKey key);

        Task<ICollection<T>> DeleteAsync(Expression<Func<T, bool>> predicate);
    }

    public interface IRepositoryAsync<T> : IRepositoryAsync<T, Guid>
        where T : BaseEntity
    {
    }
}