using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AwsS3.Application.Entities;
using System.Reflection;
using AwsS3.Application.Interfaces;
using AwsS3.Application.Services;
using AwsS3.Domain.Entities;
using AwsS3.Domain.Interfaces;
using IStatusService = AwsS3.Application.Interfaces.IStatusService;

namespace AwsS3.Application
{
    public static class ServiceCollectionConfiguration
    {
        /// <summary>
        /// Register all your interfaces and implementations here
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IAwsS3Service, AwsS3Service>();

            services.Configure<S3Settings>(configuration.GetSection("AwsS3"));

            return services;
        }
    }
}