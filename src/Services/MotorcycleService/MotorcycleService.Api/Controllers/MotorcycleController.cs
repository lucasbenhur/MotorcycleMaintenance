using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Specs;
using Shared.AppLog.Services;
using Shared.Responses;
using Shared.ServiceContext;
using Swashbuckle.AspNetCore.Annotations;

namespace MotorcycleService.Api.Controllers
{
    [ApiController]
    [Tags("motos")]
#if DEBUG
    [Route("motos")]
#else
    [Route("")]
#endif
    public class MotorcycleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public MotorcycleController(
            IAppLogger logger,
            IMediator mediator,
            IServiceContext serviceContext)
        {
            _logger = logger;
            _mediator = mediator;
            _serviceContext = serviceContext;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar uma nova moto")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMotorcycleCommand createMotorcycleCommand)
        {
            if (createMotorcycleCommand is null)
                return BadRequest(new MessageResponse());

            var motorcycle = await _mediator.Send(createMotorcycleCommand);

            if (motorcycle is null)
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Created(string.Empty, null);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Consultar motos existentes")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ICollection<MotorcycleResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMotorcyclesSpecParams specParams)
        {
            var query = new GetAllMotorcyclesQuery(specParams);
            return Ok(await _mediator.Send(query));
        }

        [HttpPut("{id}/placa")]
        [SwaggerOperation(Summary = "Modificar a placa de uma moto")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateMotorcycleCommand updateMotorcycleCommand)
        {
            if (updateMotorcycleCommand is null)
                return BadRequest(new MessageResponse());

            updateMotorcycleCommand.Id = id;

            if (!await _mediator.Send(updateMotorcycleCommand))
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Ok(new MessageResponse("Placa modificada com sucesso"));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar motos existentes por id")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var query = new GetMotorcycleByIdQuery(id);
            var motorcycle = await _mediator.Send(query);

            if (_serviceContext.HasNotification())
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            if (motorcycle is null)
                return NotFound(new MessageResponse($"A moto id {id} não existe"));

            return Ok(motorcycle);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remover uma moto")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deleteMotorcycleCommand = new DeleteMotorcycleCommand(id);

            if (!await _mediator.Send(deleteMotorcycleCommand))
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Ok();
        }
    }
}
