using BLL.Results;
using BLL.Models;

namespace BLL.Contracts
{
    public interface IWorkingProgramService
    {
        Task<Result<Guid>> CreateAsync(WorkingProgramCreateModel model);
    }
}