using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Service.EventHandlers.Commands;
using System.Threading.Tasks;

namespace OmnichannelBD.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeviceVerificationController : ControllerBase
    {
        private readonly ILogger<DeviceVerificationController> _logger;
        private readonly IMediator _mediator;

        public DeviceVerificationController(
                ILogger<DeviceVerificationController> logger,
                IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeviceVerificarionCreateCommand notification)
        {
            _logger.LogInformation("In DeviceVerificationController -> Create");

            await _mediator.Publish(notification);
            return Ok();
        }
    }
}
