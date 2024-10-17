using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Tags("entregadores")]
    [Route("entregadores")]
    public class DeliveryManController : ControllerBase
    {
        private readonly ILogger<DeliveryManController> _logger;

        public DeliveryManController(
            ILogger<DeliveryManController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Hello, World!");
            return Ok("Hello, World!");
        }
    }
}
