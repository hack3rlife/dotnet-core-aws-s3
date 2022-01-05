namespace AwsS3.Application.Entities.Dtos
{
    public class AwsS3GetObjectRequest
    {
        public string BucketName { get; set; }
        public string Key { get; set; }
    }
}
