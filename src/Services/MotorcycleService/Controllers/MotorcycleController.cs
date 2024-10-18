using Microsoft.AspNetCore.Mvc;

namespace MotorcycleService.Controllers
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
