using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Tags("locação")]
    [Route("locacao")]
    public class RentController : ControllerBase
    {
        private readonly ILogger<RentController> _logger;

        public RentController(
            ILogger<RentController> logger)
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
