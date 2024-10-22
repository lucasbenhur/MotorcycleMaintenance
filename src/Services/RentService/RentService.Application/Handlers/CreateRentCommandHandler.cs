using MediatR;
using Microsoft.Extensions.Logging;
using RentService.Application.Commands;
using RentService.Application.Mappers;
using RentService.Application.Responses;
using RentService.Core.Entities;
using RentService.Core.Integrations;
using RentService.Core.Repositories;
using Shared.ServiceContext;

namespace RentService.Application.Handlers
{
    public class CreateRentCommandHandler : IRequestHandler<CreateRentCommand, RentResponse?>
    {
        private readonly ILogger<CreateRentCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IServiceContext _serviceContext;
        private readonly IRentRepository _rentRepository;
        private readonly IDeliveryManApi _deliveryManService;
        private readonly IMotorcycleApi _motorcycleService;

        public CreateRentCommandHandler(
            ILogger<CreateRentCommandHandler> logger,
            IMediator mediator,
            IServiceContext serviceContext,
            IRentRepository rentRepository,
            IDeliveryManApi deliveryManService,
            IMotorcycleApi motorcycleService)
        {
            _logger = logger;
            _mediator = mediator;
            _serviceContext = serviceContext;
            _rentRepository = rentRepository;
            _deliveryManService = deliveryManService;
            _motorcycleService = motorcycleService;
        }

        public async Task<RentResponse?> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return null;

                var rentEntity = RentMapper.Mapper.Map<Rent>(request);
                var newRent = await _rentRepository.CreateAsync(rentEntity);

                _logger.LogInformation("Locação Id {Id} cadastrada", newRent.Id);

                return RentMapper.Mapper.Map<RentResponse>(newRent);
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao cadastrar a locação entregador_id {request.DeliveryManId} moto_id {request.MotorcycleId}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }

        private async Task<bool> IsValidAsync(CreateRentCommand request)
        {
            var today = DateTime.Now;

            if (!request.Plan.HasValue)
                _serviceContext.AddNotification("Nenhum plano foi escolhido");
            else if (!Enum.IsDefined(typeof(RentPlan), request.Plan))
                _serviceContext.AddNotification("O plano escolhido não é válido");

            if (!request.StartDate.HasValue)
                _serviceContext.AddNotification("A data de início deve ser informada");
            else if (today.AddDays(1).Date != request.StartDate.Value.Date)
                _serviceContext.AddNotification("A data de início deve ser o primeiro dia após a data da locação");

            if (string.IsNullOrWhiteSpace(request.DeliveryManId))
                _serviceContext.AddNotification("O campo entregador_id deve ser informado");
            else if (!await ExistsDeliveryMan(request.DeliveryManId!))
                _serviceContext.AddNotification($"O entregador_id {request.DeliveryManId} não existe");
            else if (!(await GetDeliveryManCnhType(request.DeliveryManId!)).Contains("A"))
                _serviceContext.AddNotification("O entregador precisa ter CNH com categoria A");

            if (string.IsNullOrWhiteSpace(request.MotorcycleId))
                _serviceContext.AddNotification("O campo moto_id deve ser informado");
            else if (!(await ExistsMotorcycle(request.MotorcycleId!)))
                _serviceContext.AddNotification($"A moto_id {request.MotorcycleId} não existe");

            return !_serviceContext.HasNotification();
        }

        private async Task<bool> ExistsDeliveryMan(string deliveryManId) =>
             !string.IsNullOrEmpty((await _deliveryManService.Get(deliveryManId!))?.Id);

        private async Task<string> GetDeliveryManCnhType(string deliveryManId) =>
            (await _deliveryManService.Get(deliveryManId))?.CnhType ?? "";

        private async Task<bool> ExistsMotorcycle(string motorcycleId) =>
             !string.IsNullOrEmpty((await _motorcycleService.Get(motorcycleId!))?.Id);
    }
}
