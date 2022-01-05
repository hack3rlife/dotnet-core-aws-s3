using Microsoft.EntityFrameworkCore;
using AwsS3.Domain.Entities;
using AwsS3.Domain.Interfaces;
using System.Threading.Tasks;

namespace AwsS3.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly CleanArchitectureDbContext _cleanArchitectureDbContext;

        public StatusRepository(CleanArchitectureDbContext cleanArchitectureDbContext)
        {
            _cleanArchitectureDbContext = cleanArchitectureDbContext;
        }

        public async Task<Status> GetStatusAsync()
        {
            return await _cleanArchitectureDbContext.Status.FirstOrDefaultAsync();
        }
    }
}