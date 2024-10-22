using MediatR;
using Microsoft.Extensions.Logging;
using RentalService.Core.Repositories;
using RentService.Application.Commands;
using RentService.Application.Mappers;
using RentService.Application.Responses;
using RentService.Application.Services;
using RentService.Core.Entities;
using Shared.ServiceContext;

namespace RentService.Application.Handlers
{
    public class RentMotorcycleCommandHandler : IRequestHandler<RentMotorcycleCommand, RentalResponse?>
    {
        private readonly ILogger<RentMotorcycleCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IServiceContext _serviceContext;
        private readonly IRentalRepository _rentalRepository;
        private readonly IDeliveryManService _deliveryManService;
        private readonly IMotorcycleService _motorcycleService;

        public RentMotorcycleCommandHandler(
            ILogger<RentMotorcycleCommandHandler> logger,
            IMediator mediator,
            IServiceContext serviceContext,
            IRentalRepository rentalRepository,
            IDeliveryManService deliveryManService,
            IMotorcycleService motorcycleService)
        {
            _logger = logger;
            _mediator = mediator;
            _serviceContext = serviceContext;
            _rentalRepository = rentalRepository;
            _deliveryManService = deliveryManService;
            _motorcycleService = motorcycleService;
        }

        public async Task<RentalResponse?> Handle(RentMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return null;

                var rentalEntity = RentalMapper.Mapper.Map<Rental>(request);
                var newRental = await _rentalRepository.CreateAsync(rentalEntity);

                _logger.LogInformation("Rental Id {Id} cadastrada", newRental.Id);

                return RentalMapper.Mapper.Map<RentalResponse>(newRental);
            } catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao cadastrar a locação entregador_id {request.DeliveryManId} moto_id {request.MotorcycleId}. Detalhes: {ex.Message}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }

        private async Task<bool> IsValidAsync(RentMotorcycleCommand request)
        {
            var today = DateTime.Now;

            if (!request.Plan.HasValue)
                _serviceContext.AddNotification("Nenhum plano foi escolhido");
            else if (!Enum.IsDefined(typeof(RentalPlan), request.Plan))
                _serviceContext.AddNotification("O plano escolhido não é válido");

            if (!request.StartDate.HasValue)
                _serviceContext.AddNotification("A data de início deve ser informada");
            else if (today.AddDays(1).Date != request.StartDate.Value.Date)
                _serviceContext.AddNotification("A data de início deve ser o primeiro dia após a data da locação");

            if (!string.IsNullOrWhiteSpace(request.DeliveryManId))
                _serviceContext.AddNotification("O id do entregador deve ser informado");
            else if (!(await GetDeliveryManCnhType(request.DeliveryManId!)).Contains("A"))
                _serviceContext.AddNotification("O entregador precisa ter CNH com categoria A");

            if (!string.IsNullOrWhiteSpace(request.MotorcycleId))
                _serviceContext.AddNotification("O id da moto deve ser informado");
            else if (!(await IsThereMotorcycle(request.MotorcycleId!)))
                _serviceContext.AddNotification("A moto informada é inválida");

            // validar se a moto já está locada

            return !_serviceContext.HasNotification();
        }

        private async Task<string> GetDeliveryManCnhType(string deliveryManId) =>
            (await _deliveryManService.Get(deliveryManId))?.CnhType ?? "";

        private async Task<bool> IsThereMotorcycle(string motorcycleId) =>
             !string.IsNullOrEmpty((await _motorcycleService.Get(motorcycleId!))?.Id);
    }
}
