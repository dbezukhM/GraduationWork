using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface IEmailSender
    {
        Task<Result> SendEmailAsync(SendEmailModel model);
    }
}