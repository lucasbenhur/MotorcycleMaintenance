using Microsoft.AspNetCore.Mvc;

namespace DeliveryMan.Controllers
{
    [ApiController]
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
        public OkObjectResult Get()
        {
            return Ok(new { Teste = "Teste" });
        }
    }
}
