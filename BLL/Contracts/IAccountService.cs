using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface IAccountService
    {
        Task<Result<string>> LoginAsync(LoginModel model);
    }
}