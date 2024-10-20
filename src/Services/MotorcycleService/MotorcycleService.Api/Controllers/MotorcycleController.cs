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
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateMotorcycle([FromBody] PublishEventCreateMotorcycleCommand? createMotorcycleEventCommand)
        {
            if (createMotorcycleEventCommand is null)
                return BadRequest(new MessageResponse("Informe um payload válido!"));

            if (await _mediator.Send<bool>(createMotorcycleEventCommand))
                return Created(string.Empty, null);
            else
                return BadRequest(new MessageResponse(_serviceContext.Notification));
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Consultar motos existentes")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ICollection<MotorcycleResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<MotorcycleResponse>>> GetAll([FromQuery] GetAllMotorcycleSpecParams specParams)
        {
            var query = new GetAllMotorcyclesQuery(specParams);
            return Ok(await _mediator.Send<ICollection<MotorcycleResponse>>(query));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar motos existentes por id")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotorcycleResponse>> GetById([FromRoute] string id)
        {
            var query = new GetMotorcycleByIdQuery(id);
            var motorcycle = await _mediator.Send<MotorcycleResponse>(query);

            if (_serviceContext.HasNotification())
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            if (motorcycle is null)
                return NotFound(new MessageResponse("Moto não encontrada"));

            return Ok(motorcycle);
        }
    }
}
