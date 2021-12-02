using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;

namespace FunctionApp1
{
    public static class UploadFunction
    {
        [FunctionName("UploadFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var formdata=await req.ReadFormAsync();
            var file=formdata.Files["file"];
            string connectionString = "UseDevelopmentStorage=true";
            BlobContainerClient container = new BlobContainerClient(connectionString, "samples-workitems");
            try
            {
                BlobClient blobClient = container.GetBlobClient(file.FileName);
                await blobClient.UploadAsync(file.OpenReadStream());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);  
            }
            return new OkObjectResult("Upload OK!");
        }
    }
}
