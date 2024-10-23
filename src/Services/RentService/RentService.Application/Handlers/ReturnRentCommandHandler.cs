using MediatR;
using RentService.Application.Commands;
using RentService.Application.Queries;
using RentService.Core.Entities;
using RentService.Core.Enums;
using RentService.Core.Integrations;
using RentService.Core.Repositories;
using Shared.AppLog.Services;
using Shared.ServiceContext;

namespace RentService.Application.Handlers
{
    public class ReturnRentCommandHandler : IRequestHandler<ReturnRentCommand, bool>
    {
        private readonly IAppLogger _logger;
        private readonly IMediator _mediator;
        private readonly IServiceContext _serviceContext;
        private readonly IRentRepository _rentRepository;
        private readonly IDeliveryManApi _deliveryManApi;
        private readonly IMotorcycleApi _motorcycleApi;

        public ReturnRentCommandHandler(
            IAppLogger logger,
            IMediator mediator,
            IServiceContext serviceContext,
            IRentRepository rentRepository,
            IDeliveryManApi deliveryManApi,
            IMotorcycleApi motorcycleApi)
        {
            _logger = logger;
            _mediator = mediator;
            _serviceContext = serviceContext;
            _rentRepository = rentRepository;
            _deliveryManApi = deliveryManApi;
            _motorcycleApi = motorcycleApi;
        }

        public async Task<bool> Handle(ReturnRentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return false;

                var rent = await _rentRepository.GetAsync(request.Id!);
                var dailyValue = CalculateAndGetDailyValue((DateTime)request.ReturnDate!, rent!);
                rent!.SetReturn((DateTime)request.ReturnDate!, dailyValue);

                if (!await _rentRepository.UpdateAsync(rent!))
                {
                    _serviceContext.AddNotification($"Não foi possível atualizar a locação Id {request.Id}");
                    return false;
                }

                _logger.LogInformation($"Locação Id {request.Id} data de devolução atualizada e valor calculado");
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao informar a data de devolução e calcular o valor para a locação Id {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(ReturnRentCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                _serviceContext.AddNotification("O campo id é obrigatório");
            else if (!await ExistsIdAsync(request.Id))
                _serviceContext.AddNotification($"A locação id {request.Id} não existe");

            if (!request.ReturnDate.HasValue)
                _serviceContext.AddNotification("Informe a data de devolução");

            return !_serviceContext.HasNotification();
        }

        private async Task<bool> ExistsIdAsync(string id)
        {
            var query = new GetRentByIdQuery(id);
            return await _mediator.Send(query) is not null;
        }

        private int CalculateAndGetDailyValue(DateTime returnDate, Rent rent)
        {
            var daysRented = (rent.EndDate - rent.StartDate).Days;
            var daysLate = (returnDate - rent.EstimatedEndDate).Days;
            var daysEarly = (rent.EstimatedEndDate - returnDate).Days;

            int dailyRate = rent.Plan switch
            {
                RentPlan.Seven => 30,
                RentPlan.Fifteen => 28,
                RentPlan.Thirty => 22,
                RentPlan.FortyFive => 20,
                RentPlan.Fifty => 18,
                _ => throw new InvalidOperationException("Plano inválido.")
            };

            int dailyValue = daysRented * dailyRate;

            if (daysLate > 0)
            {
                dailyValue += daysLate * 50;
            }
            else if (daysEarly > 0)
            {
                var missedDaysCost = daysEarly * dailyRate;
                double penaltyRate = rent.Plan switch
                {
                    RentPlan.Seven => 0.20,
                    RentPlan.Fifteen => 0.40,
                    _ => 0.00
                };

                dailyValue += (int)(missedDaysCost * penaltyRate);
            }

            return dailyValue;
        }
    }
}
