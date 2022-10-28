using CardStorageService.Data;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepositoryService clientRepositoryService;
        private readonly ILogger<ClientController> logger;
        private readonly IValidator<CreateClientRequest> validator;

        public ClientController(ILogger<ClientController> Logger, IClientRepositoryService ClientRepositoryService, IValidator<CreateClientRequest> Validator)
        {
            validator = Validator;
            clientRepositoryService = ClientRepositoryService;
            logger = Logger;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            try
            {
                var clientId = clientRepositoryService.Create(new Client
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic
                });
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Create client error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create clinet error."
                });
            }
        }
    }
}
