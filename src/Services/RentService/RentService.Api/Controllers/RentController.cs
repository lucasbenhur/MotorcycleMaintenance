using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentService.Application.Commands;
using RentService.Application.Queries;
using RentService.Application.Responses;
using Shared.Responses;
using Shared.ServiceContext;
using Swashbuckle.AspNetCore.Annotations;

namespace RentService.Api.Controllers
{
    [Tags("locação")]
#if DEBUG
    [Route("locacao")]
#else
    [Route("")]
#endif
    public class RentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RentController> _logger;
        private readonly IServiceContext _serviceContext;

        public RentController(
            ILogger<RentController> logger,
            IMediator mediator,
            IServiceContext serviceContext)
        {
            _logger = logger;
            _mediator = mediator;
            _serviceContext = serviceContext;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Alugar uma moto")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RentMotorcycle([FromBody] CreateRentCommand? createRentCommand)
        {
            if (createRentCommand is null)
                return BadRequest(new MessageResponse());

            var rent = await _mediator.Send(createRentCommand);

            if (rent is null)
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Created(string.Empty, null);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar locação por id")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var query = new GetRentByIdQuery(id);
            var rent = await _mediator.Send(query);

            if (_serviceContext.HasNotification())
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            if (rent is null)
                return NotFound(new MessageResponse("Locação não encontrada"));

            return Ok(rent);
        }

        [HttpGet("{motoId}/moto")]
        [SwaggerIgnore]
        [SwaggerOperation(Summary = "Consultar locação por id da moto")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByMotorcycleId([FromRoute] string motoId)
        {
            var query = new GetRentByMotorcycleIdQuery(motoId);
            var rent = await _mediator.Send(query);

            if (_serviceContext.HasNotification())
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            if (rent is null)
                return NotFound(new MessageResponse("Locação não encontrada"));

            return Ok(rent);
        }

        [HttpPut("{id}/devolucao")]
        [SwaggerOperation(Summary = "Informar data de devolução e calcular valor")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Return([FromRoute] string id, [FromBody] ReturnRentCommand? returnRentCommand)
        {
            if (returnRentCommand is null)
                return BadRequest(new MessageResponse());

            returnRentCommand.Id = id;

            if (!await _mediator.Send(returnRentCommand))
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Ok(new MessageResponse("Data de devolução informada com sucesso"));
        }
    }
}
