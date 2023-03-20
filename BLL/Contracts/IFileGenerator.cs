using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface IFileGenerator
    {
        Task<Result<WorkingProgramModel>> GenerateFile(Guid subjectId);
    }
}