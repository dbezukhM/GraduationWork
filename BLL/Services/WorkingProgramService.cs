using BLL.Contracts;
using BLL.Models;
using BLL.Results;

namespace BLL.Services
{
    public class WorkingProgramService : IWorkingProgramService
    {
        public Task<Result<Guid>> CreateAsync(WorkingProgramCreateModel model)
        {
            throw new NotImplementedException();
        }
    }
}