using Amazon.S3;
using Amazon.S3.Model;
using dotnet.core.aws.serverless.s3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace dotnet.core.aws.serverless.s3.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;

        public FilesController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        [HttpGet("View")]
        public async Task<IActionResult> Download([FromQuery] string bucketName, string key)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = key
            };

            var response = await _amazonS3.GetObjectAsync(getObjectRequest);

            await using var responseStream = response.ResponseStream;

            var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();

            return File(bytes, response.Headers["Content-Type"]);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] string bucketName, string key)
        {
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            var response = await _amazonS3.GetObjectAsync(getObjectRequest);

            await using var responseStream = response.ResponseStream;

            var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();

            var value = new AwsS3GetObjectResponse()
            {
                HttpStatusCode = response.HttpStatusCode,
                ContentBody = bytes,
                ContentType = response.Headers["Content-Type"]
            };

            return Ok(value);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile file, string bucketName)
        {
            var putObjectRequest = new PutObjectRequest
            {
                InputStream = file.OpenReadStream(),
                BucketName = bucketName,
                ContentType = file.ContentType,
                Key = file.FileName
            };

            var putObjectResponse = await _amazonS3.PutObjectAsync(putObjectRequest);

            return Created(bucketName, putObjectResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] JsonElement jsonElement)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = jsonElement.GetProperty("bucketName").ToString(),
                Key = jsonElement.GetProperty("key").ToString()
            };

            var response = await _amazonS3.DeleteObjectAsync(deleteObjectRequest);

            return new ObjectResult(string.Empty)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
    }
}
