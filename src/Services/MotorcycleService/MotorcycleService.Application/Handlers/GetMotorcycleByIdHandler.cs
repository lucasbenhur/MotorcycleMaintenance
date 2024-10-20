using MediatR;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Repositories;

namespace MotorcycleService.Application.Handlers
{
    public class GetMotorcycleByIdHandler : IRequestHandler<GetMotorcycleByIdQuery, MotorcycleResponse>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public GetMotorcycleByIdHandler(
            IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<MotorcycleResponse> Handle(GetMotorcycleByIdQuery request, CancellationToken cancellationToken)
        {
            var motorcycle = await _motorcycleRepository.GetAsync(request.Id);
            return MotorcycleMapper.Mapper.Map<MotorcycleResponse>(motorcycle);
        }
    }
}
