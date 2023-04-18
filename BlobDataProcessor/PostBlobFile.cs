using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using System.Web.Http;
using System;
using BlobDataProcessor.Models;

namespace BlobDataProcessor
{
    public static class PostBlobFile
    {
        [FunctionName("PostBlobFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PostBlobFile")]
            BlobFilePostRequest model,
            HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var blobConnection = config["Storage"];
            var container = config["Container"];

            if (string.IsNullOrWhiteSpace(model.FileName) || model.Contents == null)
            {
                return new BadRequestErrorMessageResult("File or file name is missing");
            }

            try
            {
                var containerClient = new BlobContainerClient(blobConnection, container);
                var blobClient = containerClient.GetBlobClient($"{model.FileName}.docx");

                using var stream = new MemoryStream(model.Contents);
                await blobClient.UploadAsync(stream);

                return new OkResult();
            }
            catch (Exception ex)
            {
                var error = "Failed uploading file to Blob Storage";
                log.LogError(error);
                log.LogError(ex, ex.Message);

                return new BadRequestErrorMessageResult(ex.Message);
            }
        }
    }
}