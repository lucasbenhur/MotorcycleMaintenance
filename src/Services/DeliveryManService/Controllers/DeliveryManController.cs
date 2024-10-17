using Microsoft.AspNetCore.Mvc;

namespace DeliveryManService.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            return Ok("Hello World!");
        }
    }
}
