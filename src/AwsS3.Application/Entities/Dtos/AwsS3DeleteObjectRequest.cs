namespace AwsS3.Application.Entities.Dtos
{
    public class AwsS3DeleteObjectRequest
    {
        public string BucketName { get; set; }
        public string Key { get; set; }
    }
}
