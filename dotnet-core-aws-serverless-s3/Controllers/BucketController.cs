using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace dotnet.core.aws.serverless.s3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        private readonly ILogger<BucketController> _logger;
        private readonly IAmazonS3 _amazonS3;

        public BucketController(IAmazonS3 amazonS3, ILogger<BucketController> logger)
        {
            _amazonS3 = amazonS3;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<S3Bucket>>> Get()
        {
            _logger.LogInformation("Calling Get All Buckets");

            var response = await _amazonS3.ListBucketsAsync();

            var buckets = response.Buckets;

            return Ok(buckets);
        }

        [HttpGet("Files")]
        public async Task<ActionResult<ListObjectsV2Response>> GetByBucketName([FromQuery] string bucketName)
        {
            _logger.LogInformation("Calling Get Files by Bucket Name");

            var listObjectsV2Request = new ListObjectsV2Request
            {
                BucketName = bucketName,
            };

            var bucket = await _amazonS3.ListObjectsV2Async(listObjectsV2Request);

            return Ok(bucket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBucket([FromBody] JsonElement jsonElement)
        {
            var bucketName = jsonElement.GetProperty("bucketName").ToString();
            var bucketRegionName = jsonElement.GetProperty("region").ToString();

            _logger.LogInformation($"Creating Bucket: {bucketName}");

            var putBucketRequest = new PutBucketRequest
            {
                BucketName = bucketName,
                BucketRegionName = bucketRegionName 
            };

            var putBucketResponse = await _amazonS3.PutBucketAsync(putBucketRequest);

            return CreatedAtAction(nameof(CreateBucket), putBucketRequest.BucketName);
        }
    }
}