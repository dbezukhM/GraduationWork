using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface IFileProvider
    {
        Task<Result<BlobFileGetModel>> GetFileAsync(string fullFileName);
    }
}