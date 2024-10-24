﻿using MassTransit;
using MediatR;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Queries;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Core.Specs;
using Shared.AppLog.Services;
using Shared.Extensions;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class UpdateMotorcycleCommandHandler : IRequestHandler<UpdateMotorcycleCommand, bool>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateMotorcycleCommandHandler(
            IMotorcycleRepository motorcycleRepository,
            IAppLogger logger,
            IServiceContext serviceContext,
            IMediator mediator,
            IPublishEndpoint publishEndpoint)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _serviceContext = serviceContext;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return false;

                var motorcycle = await _motorcycleRepository.GetAsync(request.Id!);

                if (motorcycle == null)
                    throw new NullReferenceException();

                motorcycle.UpdatePlate(request.Plate);

                if (!await _motorcycleRepository.UpdateAsync(motorcycle))
                {
                    _serviceContext.AddNotification($"Não foi possível atualizar a Moto ID {request.Id}");
                    return false;
                }

                _logger.LogInformation($"Moto ID {motorcycle.Id} atualizada");
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao atualizar a Moto ID {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(UpdateMotorcycleCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                _serviceContext.AddNotification("O campo id é obrigatório");
            else if (!await ExistsIdAsync(request.Id))
                _serviceContext.AddNotification($"A moto id {request.Id} não existe");

            if (string.IsNullOrWhiteSpace(request.Plate))
                _serviceContext.AddNotification("O campo placa é obrigatório");
            else if (!request.Plate.IsPlate())
                _serviceContext.AddNotification("Informe uma placa válida!");
            else if (await ExistsPlateAsync(request))
                _serviceContext.AddNotification($"A placa {request.Plate} já existe");

            return !_serviceContext.HasNotification();
        }

        private async Task<bool> ExistsPlateAsync(UpdateMotorcycleCommand request)
        {
            var specParams = new GetAllMotorcyclesSpecParams(request.Plate);
            var query = new GetAllMotorcyclesQuery(specParams);
            return (await _mediator.Send(query)).Any(x => x.Id.ToUpper() != request.Id?.ToUpper());
        }

        private async Task<bool> ExistsIdAsync(string id)
        {
            var query = new GetMotorcycleByIdQuery(id);
            return await _mediator.Send(query) is not null;
        }
    }
}
