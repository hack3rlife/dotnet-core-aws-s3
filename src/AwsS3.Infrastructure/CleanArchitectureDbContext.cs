using Microsoft.EntityFrameworkCore;
using AwsS3.Domain.Entities;

namespace AwsS3.Infrastructure
{
    public class CleanArchitectureDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CleanArchitectureDbContext(DbContextOptions<CleanArchitectureDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Status> Status { get; set; }

    }
}
