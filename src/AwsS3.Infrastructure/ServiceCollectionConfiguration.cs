using AwsS3.Application.Interfaces;
using AwsS3.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IStatusRepository = AwsS3.Domain.Interfaces.IStatusRepository;

namespace AwsS3.Infrastructure
{
    public static class ServiceCollectionConfiguration
    {
        /// <summary>
        /// Register all your Interfaces and its implementations here
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IAwsS3Client, AwsS3Client>();

            services.AddDbContext<CleanArchitectureDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "in-memory");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            return services;
        }
    }
}