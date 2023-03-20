using System.Net;
using System.Text;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using BLL.Settings;
using Microsoft.AspNetCore.Http;
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
            var url = _settings.AzureFunctionGetFileUrl.Replace("{fullFileName}", fullFileName);

            var result = await client.GetAsync(url);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return Result.NotFound<BlobFileGetModel>(BlErrors.FileNotFound);
            }

            var responseContent = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BlobFileGetModel>(responseContent);

            return Result.Success(model!);
        }

        public async Task<Result<Guid>> PostFileAsync(IFormFile file)
        {
            var blobFileId = Guid.NewGuid();
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var request = new
            {
                FileName = blobFileId.ToString(),
                Contents = stream.ToArray(),
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var result = await client.PostAsync(_settings.AzureFunctionPostFileUrl, stringContent);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return Result.Failure<Guid>(BlErrors.FileNotFound);
            }

            return Result.Success(blobFileId);
        }
    }
}