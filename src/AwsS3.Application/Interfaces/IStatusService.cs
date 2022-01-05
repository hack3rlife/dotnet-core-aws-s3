using System.Threading.Tasks;
using AwsS3.Application.Entities.Dtos;

namespace AwsS3.Application.Interfaces
{
    public interface IStatusService
    {
        /// <summary>
        /// Get the status of the service
        /// </summary>
        /// <returns>The <see cref="StatusResponse"/></returns>
        Task<StatusResponse> GetStatusAsync();
    }
}
