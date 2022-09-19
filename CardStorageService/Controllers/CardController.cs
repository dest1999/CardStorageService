using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> logger;

        public CardController(ILogger<CardController> Logger)
        {
            logger = Logger;
        }

        public IActionResult GetByClientId(string clientId)
        {
            return Ok();
        }

    }
}
