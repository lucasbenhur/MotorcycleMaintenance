using DeliveryManService.Application.Commands;
using DeliveryManService.Application.Queries;
using DeliveryManService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.AppLog.Services;
using Shared.Responses;
using Shared.ServiceContext;
using Swashbuckle.AspNetCore.Annotations;

namespace DeliveryManService.Api.Controllers
{
    [ApiController]
    [Tags("entregadores")]
#if DEBUG
    [Route("entregadores")]
#else
    [Route("")]
#endif
    public class DeliveryManController : ControllerBase
    {
        private readonly IAppLogger _logger;
        private readonly IMediator _mediator;
        private readonly IServiceContext _serviceContext;

        public DeliveryManController(
            IAppLogger logger,
            IMediator mediator,
            IServiceContext serviceContext)
        {
            _logger = logger;
            _mediator = mediator;
            _serviceContext = serviceContext;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar entregador")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateDeliveryManCommand createDeliveryManCommand)
        {
            if (createDeliveryManCommand is null)
                return BadRequest(new MessageResponse());

            if (!await _mediator.Send(createDeliveryManCommand))
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Created(string.Empty, null);
        }

        [HttpPost("/{id}/cnh")]
        [SwaggerOperation(Summary = "Enviar foto da CNH")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateDeliveryManCommand updateDeliveryManCommand)
        {
            if (updateDeliveryManCommand is null)
                return BadRequest(new MessageResponse());

            updateDeliveryManCommand.Id = id;

            if (!await _mediator.Send(updateDeliveryManCommand))
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Created(string.Empty, null);
        }

        [HttpGet("{id}")]
        [SwaggerIgnore]
        [SwaggerOperation(Summary = "Consultar entregadores existentes por id")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DeliveryMan), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var query = new GetDeliveryManByIdQuery(id);
            var deliveryMan = await _mediator.Send(query);

            if (_serviceContext.HasNotification())
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            if (deliveryMan is null)
                return NotFound(new MessageResponse("Entregador não encontrado"));

            return Ok(deliveryMan);
        }
    }
}
