using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using AwsS3.Application.Interfaces;
using AwsS3.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using AwsS3.Application.Entities.Dtos;

namespace AwsS3.Infrastructure
{
    public class AwsS3Client : IAwsS3Client
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly S3Settings _s3Settings;


        public AwsS3Client(IOptions<S3Settings> s3Settings)
        {
            _s3Settings = s3Settings.Value;

            var region = RegionEndpoint.GetBySystemName(_s3Settings.AWSRegion);

            _amazonS3 = new AmazonS3Client(awsAccessKeyId: _s3Settings.AWSAccessKey,
                awsSecretAccessKey: _s3Settings.AWSSecretKey, region: region);
        }

        public async Task<PutObjectResponse> UpsertObjectAsync(PutObjectRequest putObjectRequest)
        {
            try
            {
                return await _amazonS3.PutObjectAsync(putObjectRequest);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<AwsS3GetObjectResponse> GetObjectAsync(GetObjectRequest getObjectRequest)
        {
            try
            {
                using var response = await _amazonS3.GetObjectAsync(getObjectRequest);
                await using var responseStream = response.ResponseStream;

                var memoryStream = new MemoryStream();
                await responseStream.CopyToAsync(memoryStream);
                var bytes = memoryStream.ToArray();

                return new AwsS3GetObjectResponse
                {
                    ContentType = response.Headers["Content-Type"],
                    ContentBody = bytes,
                    HttpStatusCode = response.HttpStatusCode
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<DeleteObjectResponse> DeleteAsync(DeleteObjectRequest deleteObjectsRequest)
        {
            try
            {
                var deleteObjectResponse = await _amazonS3.DeleteObjectAsync(deleteObjectsRequest);
                return deleteObjectResponse;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

    }
}