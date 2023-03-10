using BLL.Results;
using DAL.Entities;

namespace BLL.Contracts
{
    public interface ITokenGenerator
    {
        Task<Result<string>> GetToken(Person person);
    }
}