using BLL.Models;
using BLL.Results;
using Microsoft.AspNetCore.Http;

namespace BLL.Contracts
{
    public interface IFileProvider
    {
        Task<Result<BlobFileGetModel>> GetFileAsync(string fullFileName);

        Task<Result<Guid>> PostFileAsync(IFormFile file);
    }
}