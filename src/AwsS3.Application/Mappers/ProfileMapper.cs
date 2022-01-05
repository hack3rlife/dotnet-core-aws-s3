using Amazon.S3.Model;
using AutoMapper;
using AwsS3.Application.Entities.Dtos;
using AwsS3.Application.Entities.Dtos.Request;
using AwsS3.Application.Entities.Dtos.Response;
using AwsS3.Domain.Entities;

namespace AwsS3.Application.Mappers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Status, StatusResponse>();

            CreateMap<AwsS3PutObjectRequest, PutObjectRequest>();
            CreateMap<AwsS3PutObjectResponse, PutObjectResponse>();
        }
    }
}
