using System.Net;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using BLL.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BLL.Services
{
    public class FileProvider : IFileProvider
    {
        private readonly ProgramSettings _settings;

        public FileProvider(
            IOptionsSnapshot<ProgramSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<Result<BlobFileGetModel>> GetFileAsync(string fullFileName)
        {
            using var client = new HttpClient();
            var url = _settings.AzureFunctionUrl.Replace("{fullFileName}", fullFileName);

            var result = await client.GetAsync(url);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return Result.NotFound<BlobFileGetModel>(BlErrors.FileNotFound);
            }

            var responseContent = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BlobFileGetModel>(responseContent);

            return Result.Success(model!);
        }
    }
}