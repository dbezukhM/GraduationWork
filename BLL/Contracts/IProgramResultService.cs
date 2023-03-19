using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface IProgramResultService
    {
        Task<Result<IEnumerable<ProgramResultGetModel>>> GetAllAsync();

        Task<Result<Guid>> CreateAsync(ProgramResultCreateModel model);

        Task<Result> UpdateAsync(ProgramResultUpdateModel model);

        Task<Result> DeleteByIdAsync(Guid id);
    }
}