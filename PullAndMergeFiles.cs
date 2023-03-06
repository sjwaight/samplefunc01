using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace samplefunc01
{
    public static class PullS3File
    {
        private static string bucketName = Environment.GetEnvironmentVariable("bucketName");
        private static string keyName = Environment.GetEnvironmentVariable("keyName");
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("regionName"));
        private static IAmazonS3 client;

        [FunctionName("PullS3File")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function received a request.");

            client = new AmazonS3Client(bucketRegion);
            string responseMessage = await ReadObjectDataAsync(bucketName, keyName, log);

            return new OkObjectResult(responseMessage);
        }

        /// <summary>
        /// Read an object's data from an Amazon S3 bucket.
        ///
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="keyName">The name of the object.</param>
        /// <param name="log">Logger</param>
        /// <returns>Object data</returns>
        private static async Task<string> ReadObjectDataAsync(string bucketName, string keyName, ILogger log)
        {
            string responseBody = "Failed to read remote file.";
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string contentType = response.Headers["Content-Type"];
                    log.LogInformation("Content type: {0}", contentType);

                    responseBody = reader.ReadToEnd();
                }
            }
            catch (AmazonS3Exception e)
            {
                // If bucket or object does not exist
                log.LogError("Error encountered ***. Message:'{0}' when reading object", e.Message);
            }
            catch (Exception e)
            {
                log.LogError("Unknown error encountered on server. Message:'{0}' when reading object", e.Message);
            }
            return responseBody;
        }
    }
}
