using AwsS3.Application.Entities.Dtos;
using AwsS3.Application.Entities.Dtos.Request;
using AwsS3.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwsS3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwsS3Controller : ControllerBase
    {
        private readonly IAwsS3Service _awsS3Service;

        public AwsS3Controller(IAwsS3Service awsS3Service)
        {
            _awsS3Service = awsS3Service;
        }

        [HttpGet("Download")]
        public async Task<IActionResult> Download([FromQuery] AwsS3GetObjectRequest awsS3GetObjectRequest)
        {
            var response = await _awsS3Service.GetAsync(awsS3GetObjectRequest);
            return File(response.ContentBody, response.ContentType);
        }

        [HttpGet]
        public async Task<AwsS3GetObjectResponse> Get([FromQuery] AwsS3GetObjectRequest awsS3GetObjectRequest)
        {
            return await _awsS3Service.GetAsync(awsS3GetObjectRequest);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] AwsS3PutObjectRequest awsS3PutObjectRequest)
        {
            var response = await _awsS3Service.UpsertAsync(awsS3PutObjectRequest);

            return new ObjectResult(response.HttpStatusCode)
            {
                StatusCode = (int)response.HttpStatusCode,
            };
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]AwsS3DeleteObjectRequest awsS3DeleteObjectRequest)
        {
            var response = await _awsS3Service.DeleteAsync(awsS3DeleteObjectRequest);
            return new ObjectResult(response.HttpStatusCode)
            {
                StatusCode = (int) response.HttpStatusCode
            };
        }
    }
}
