using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface ICrud<T> where T : IDomainModel
    {
        Task<Result<IEnumerable<T>>> GetAllAsync();

        Task<Result<T>> GetByIdAsync(Guid id);

        Task<Result<Guid>> AddAsync(T model);

        Task<Result> UpdateAsync(T model);

        Task<Result> DeleteByIdAsync(Guid modelId);
    }
}