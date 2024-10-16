using Microsoft.AspNetCore.Mvc;

namespace Motorcycle.Controllers
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
        public OkObjectResult Get()
        {
            return Ok(new { Teste = "Teste" });
        }
    }
}
