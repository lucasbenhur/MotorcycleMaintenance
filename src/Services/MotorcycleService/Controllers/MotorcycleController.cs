using Microsoft.AspNetCore.Mvc;

namespace MotorcycleService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotorcycleController : ControllerBase
    {
        private readonly ILogger<MotorcycleController> _logger;

        public MotorcycleController(
            ILogger<MotorcycleController> logger)
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
