using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentService.Application.Commands;
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
        public async Task<IActionResult> RentMotorcycle([FromBody] RentMotorcycleCommand? rentMotorcycleCommand)
        {
            if (rentMotorcycleCommand is null)
                return BadRequest(new MessageResponse());

            var rental = await _mediator.Send(rentMotorcycleCommand);

            if (rental is null)
                return BadRequest(new MessageResponse(_serviceContext.Notification));

            return Created(string.Empty, null);
        }
    }
}
