using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Storage.Blob;

namespace FunctionApp1
{
    public static class LectionFunction
    {
        [FunctionName("LectionFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "lections/{name}")] HttpRequest req,
            [Blob("samples-workitems/LAAG_Lections/{name}", FileAccess.Read, Connection = "AzureWebJobsStorage")] ICloudBlob blob,
            ILogger log)
        {
            var blobStream = await blob.OpenReadAsync().ConfigureAwait(false);
            return new FileStreamResult(blobStream, "application/octet-stream");
        }
    }
}
