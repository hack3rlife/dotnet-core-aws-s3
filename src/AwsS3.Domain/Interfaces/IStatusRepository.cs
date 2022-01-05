using System.Threading.Tasks;
using AwsS3.Domain.Entities;

namespace AwsS3.Domain.Interfaces
{
    public interface IStatusRepository
    {
        /// <summary>
        /// Gets the service status
        /// </summary>
        /// <returns>The Service <see cref="Status"/></returns>
        Task<Status> GetStatusAsync();
    }
}
