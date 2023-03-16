using BLL.Results;

namespace BLL.Contracts
{
    public interface IFileGenerator
    {
        Task<Result<MemoryStream>> GenerateFile(Guid subjectId);
    }
}