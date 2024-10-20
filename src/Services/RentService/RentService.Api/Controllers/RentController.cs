using Microsoft.AspNetCore.Mvc;

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
        private readonly ILogger<RentController> _logger;

        public RentController(
            ILogger<RentController> logger)
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
