using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using System.IO;
using System.Web.Http;
using BlobDataProcessor.Models;

namespace BlobDataProcessor
{
    public static class GetBlobFile
    {
        [FunctionName("GetBlobFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetBlobFile/{fullFileName}")]
            HttpRequest req, string fullFileName,
            ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var blobConnection = config["Storage"];
            var container = config["Container"];
            try
            {
                var containerClient = new BlobContainerClient(blobConnection, container);
                var blobClient = containerClient.GetBlobClient(fullFileName);

                using var stream = new MemoryStream();
                await blobClient.DownloadToAsync(stream);

                var response = new BlobFileResponse
                {
                    Contents = stream.ToArray(),
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                var error = "Failed downloading file from Blob Storage";
                log.LogError(error);
                log.LogError(ex, ex.Message);

                return new BadRequestErrorMessageResult(ex.Message);
            }
        }
    }
}