using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using FileUploaderAzure.Functions.ServiceContracts;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileUploaderAzure.Functions.BlobTriggerFunctions
{
    public class BlobTriggerFunction
    {
        private readonly IEmailSenderService _emailSenderService;

        public BlobTriggerFunction(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [FunctionName("BlobTriggerFunction")]
        public async Task Run([BlobTrigger("test-task-container/{name}", Connection = "AzureWebJobsStorage")] BlobClient blobClient)
        {
            var properties = await blobClient.GetPropertiesAsync();

            if (properties.Value.Metadata.ContainsKey("Email"))
            {
                string email = properties.Value.Metadata["Email"];

                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = blobClient.BlobContainerName,
                    BlobName = blobClient.Name,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);
                var sasToken = blobClient.GenerateSasUri(sasBuilder);

                await _emailSenderService.SendEmail(email, sasToken.ToString());
            }
        }
    }
}
