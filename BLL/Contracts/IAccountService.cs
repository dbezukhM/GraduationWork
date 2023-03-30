using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface IAccountService
    {
        Task<Result<TokenModel>> LoginAsync(LoginModel model);

        Task<Result> LogOut();

        Task<Result<Guid>> CreateAsync(PersonCreateModel model);

        Task<Result> ChangePasswordAsync(PersonChangePasswordModel model);

        Task<Result<PersonGetModel>> GetByIdAsync(Guid personId);

        Task<Result<PersonGetModel>> GetByEmailAsync(string email);

        Task<Result<IEnumerable<PersonGetModel>>> GetAllAsync();
    }
}