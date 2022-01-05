using Microsoft.AspNetCore.Http;

namespace AwsS3.Application.Entities.Dtos.Request
{
    public class AwsS3PutObjectRequest
    {
        public IFormFile File { get; set; }
        public string BucketName { get; set; }
        public string  Key { get; set; }

    }
}
