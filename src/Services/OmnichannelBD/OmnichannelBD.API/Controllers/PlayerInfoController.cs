using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Service.EventHandlers.Commands;
using System.Threading.Tasks;

namespace OmnichannelDB.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerInfoController : ControllerBase
    {
        private readonly ILogger<PlayerInfoController> _logger;
        private readonly IMediator _mediator;

        public PlayerInfoController(
                ILogger<PlayerInfoController> logger,
                IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayerInfoCreateCommand notification)
        {
            _logger.LogInformation("In PlayerInfoController -> Create");

            await _mediator.Publish(notification);
            return Ok();
        }
    }
}
