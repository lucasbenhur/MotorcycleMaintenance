using Microsoft.AspNetCore.Mvc;

namespace Rent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase
    {
        private readonly ILogger<RentController> _logger;

        public RentController(
            ILogger<RentController> logger)
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
