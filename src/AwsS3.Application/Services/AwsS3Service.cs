using Amazon.S3.Model;
using AutoMapper;
using AwsS3.Application.Entities.Dtos.Request;
using AwsS3.Application.Entities.Dtos.Response;
using AwsS3.Application.Interfaces;
using System.Threading.Tasks;
using AwsS3.Application.Entities.Dtos;

namespace AwsS3.Application.Services
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAwsS3Client _awsS3Client;
        private readonly IMapper _mapper;

        public AwsS3Service(IAwsS3Client awsS3Client)
        {
            _awsS3Client = awsS3Client;
        }

        public async Task<AwsS3GetObjectResponse> GetAsync(AwsS3GetObjectRequest request)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                BucketName = request.BucketName,
                Key = request.Key
            };

            var response = await _awsS3Client.GetObjectAsync(getObjectRequest);

            return new AwsS3GetObjectResponse
            {
                ContentType = response.ContentType,
                ContentBody = response.ContentBody,
                HttpStatusCode = response.HttpStatusCode
            };
        }

        public async Task<AwsS3PutObjectResponse> UpsertAsync(AwsS3PutObjectRequest request)
        {
            var putObjectRequest = new PutObjectRequest
            {
                InputStream = request.File.OpenReadStream(),
                BucketName = request.BucketName,
                ContentType = request.File.ContentType,
                Key = request.File.FileName

            };

            var putObjectResponse = await _awsS3Client.UpsertObjectAsync(putObjectRequest);

            return new AwsS3PutObjectResponse
            {
                HttpStatusCode = putObjectResponse.HttpStatusCode
            };
        }

        public async Task<AwsS3DeleteObjectResponse> DeleteAsync(AwsS3DeleteObjectRequest request)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = request.BucketName,
                Key = request.Key
            };

            var deleteObjectResponse = await _awsS3Client.DeleteAsync(deleteObjectRequest);

            return new AwsS3DeleteObjectResponse()
            {
                HttpStatusCode = deleteObjectResponse.HttpStatusCode
            };
        }
    }
}
