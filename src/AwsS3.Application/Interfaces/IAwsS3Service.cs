using System.Threading.Tasks;
using Amazon.S3.Model;
using AwsS3.Application.Entities.Dtos;
using AwsS3.Application.Entities.Dtos.Request;
using AwsS3.Application.Entities.Dtos.Response;

namespace AwsS3.Application.Interfaces
{
    /// <summary>
    /// AWS S3 available actions 
    /// </summary>
    public interface IAwsS3Service
    {
        Task<AwsS3GetObjectResponse> GetAsync(AwsS3GetObjectRequest request);
        Task<AwsS3PutObjectResponse> UpsertAsync(AwsS3PutObjectRequest request);
        Task<AwsS3DeleteObjectResponse> DeleteAsync(AwsS3DeleteObjectRequest request);

    }
}