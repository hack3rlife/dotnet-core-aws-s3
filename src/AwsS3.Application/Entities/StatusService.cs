using AutoMapper;
using AwsS3.Application.Entities.Dtos;
using System.Threading.Tasks;
using AwsS3.Domain.Interfaces;
using IStatusService = AwsS3.Application.Interfaces.IStatusService;

namespace AwsS3.Application.Entities
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<StatusResponse> GetStatusAsync()
        {
           var status = await _statusRepository.GetStatusAsync();

           return _mapper.Map<StatusResponse>(status);
        }
    }
}
