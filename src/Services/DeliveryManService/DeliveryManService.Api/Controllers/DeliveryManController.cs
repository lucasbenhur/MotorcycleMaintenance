using DeliveryManService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<DeliveryManController> _logger;
        private readonly IMediator _mediator;
        private readonly IServiceContext _serviceContext;

        public DeliveryManController(
            ILogger<DeliveryManController> logger,
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
        public async Task<IActionResult> Create([FromBody] CreateDeliveryManCommand? createDeliveryManCommand)
        {
            if (createDeliveryManCommand is null)
                return BadRequest(new MessageResponse());

            if (!await _mediator.Send<bool>(createDeliveryManCommand))
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Created(string.Empty, null);
        }

        [HttpPost("/{id}/cnh")]
        [SwaggerOperation(Summary = "Enviar foto da CNH")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateDeliveryManCommand? updateDeliveryManCommand)
        {
            if (updateDeliveryManCommand is null)
                return BadRequest(new MessageResponse());

            updateDeliveryManCommand.Id = id;

            if (!await _mediator.Send<bool>(updateDeliveryManCommand))
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Created(string.Empty, null);
        }
    }
}
