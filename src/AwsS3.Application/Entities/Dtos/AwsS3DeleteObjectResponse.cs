using System.Net;

namespace AwsS3.Application.Entities.Dtos
{
    public class AwsS3DeleteObjectResponse
    {
        public byte[] ContentBody { get; set; }
        public string ContentType { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

    }
}
