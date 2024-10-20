using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Specs;
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
        private readonly ILogger<MotorcycleController> _logger;
        private readonly IServiceContext _serviceContext;

        public MotorcycleController(
            ILogger<MotorcycleController> logger,
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
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateMotorcycle([FromBody] PublishEventCreateMotorcycleCommand? createMotorcycleEventCommand)
        {
            if (createMotorcycleEventCommand is null)
                return BadRequest(new BadRequestResponse("Informe um payload válido!"));

            if (await _mediator.Send<bool>(createMotorcycleEventCommand))
                return CreatedAtAction(nameof(CreateMotorcycle), createMotorcycleEventCommand);
            else
                return BadRequest(new BadRequestResponse(_serviceContext.Notification));
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Consultar motos existentes")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<MotorcycleResponse>>> GetAll([FromQuery] MotorcycleSpecParams motorcycleSpecParams)
        {
            var query = new GetAllMotorcyclesQuery(motorcycleSpecParams);
            return Ok(await _mediator.Send<ICollection<MotorcycleResponse>>(query));
        }
    }
}
