using BLL.Contracts;
using BLL.Models;
using BLL.Results;
using BLL.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ProgramSettings _settings;

        public EmailSender(IOptionsSnapshot<ProgramSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<Result> SendEmailAsync(SendEmailModel model)
        {
            using var client = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            await client.PostAsync(_settings.LogicAppEmailSenderUrl, stringContent);

            return Result.Success();
        }
    }
}