using Microsoft.AspNetCore.Mvc;

namespace DeliveryMenService.Controllers
{
    [ApiController]
    [Tags("entregadores")]
#if DEBUG
    [Route("entregadores")]
#else
    [Route("")]
#endif
    public class DeliveryMenController : ControllerBase
    {
        private readonly ILogger<DeliveryMenController> _logger;

        public DeliveryMenController(
            ILogger<DeliveryMenController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}
