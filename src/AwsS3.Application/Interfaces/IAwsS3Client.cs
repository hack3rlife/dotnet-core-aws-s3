using Amazon.S3.Model;
using AwsS3.Application.Entities.Dtos;
using System.Threading.Tasks;

namespace AwsS3.Application.Interfaces
{
    /// <summary>
    /// Wrapper Interface to manage communication with AWS S3 
    /// </summary>
    public interface IAwsS3Client
    {
        Task<AwsS3GetObjectResponse> GetObjectAsync(GetObjectRequest getObjectRequest);
        Task<PutObjectResponse> UpsertObjectAsync(PutObjectRequest putObjectRequest);
        Task<DeleteObjectResponse> DeleteAsync(DeleteObjectRequest deleteObjectsRequest);

    }
}
