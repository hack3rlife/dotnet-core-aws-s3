using System.Net;

namespace dotnet.core.aws.serverless.s3.Entities
{
    internal class AwsS3GetObjectResponse
    {
        public AwsS3GetObjectResponse()
        {
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public byte[] ContentBody { get; set; }
        public string ContentType { get; set; }
    }
}