using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Repositories;

namespace MotorcycleService.Application.Handlers
{
    public class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleCommand, MotorcycleResponse>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<CreateMotorcycleCommandHandler> _logger;

        public CreateMotorcycleCommandHandler(
            IMotorcycleRepository motorcycleRepository,
            ILogger<CreateMotorcycleCommandHandler> logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
        }

        public async Task<MotorcycleResponse> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycleEntity = MotorcycleMapper.Map(request);
                var newMotorcycle = await _motorcycleRepository.CreateAsync(motorcycleEntity);
                _logger.LogInformation("Moto Id {Id} armazenada para consulta futura.", newMotorcycle.Id);
                return MotorcycleMapper.Mapper.Map<MotorcycleResponse>(newMotorcycle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao armazenar a moto Id {Id}. Detalhes: {Message}.", request.Id, ex.Message);
                return null;
            }
        }
    }
}
