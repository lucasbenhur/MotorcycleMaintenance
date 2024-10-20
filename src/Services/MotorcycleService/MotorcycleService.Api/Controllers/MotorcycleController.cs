using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Responses;
using Shared.Responses;

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

        public MotorcycleController(
            ILogger<MotorcycleController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult> CreateMotorcycle([FromBody] PublishEventCreateMotorcycleCommand createMotorcycleEventCommand)
        {
            var response = await _mediator.Send<PublishEventeCreateMotorcycleResponse>(createMotorcycleEventCommand);

            if (response.Success)
                return CreatedAtAction(nameof(CreateMotorcycle), createMotorcycleEventCommand);
            else
                return BadRequest(new BadRequestResponse(response.Notification));
        }
    }
}
