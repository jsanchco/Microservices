using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Playtech.Service.Player;
using Playtech.Service.Player.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Playtech.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlaytechController : ControllerBase
    {
        private readonly ILogger<PlaytechController> _logger;
        private readonly IServicePlayer _servicePlayer;

        public PlaytechController(
            ILogger<PlaytechController> logger,
            IServicePlayer servicePlayer
            )
        {
            _logger = logger;
            _servicePlayer = servicePlayer;
        }

        [HttpGet]
        public async Task<PlayerInfo> Get(string id)
        {
            _logger.LogInformation("In PlaytechController -> Get");

            if (string.IsNullOrEmpty(id))
                return await _servicePlayer.GetPlayerInfo();

            return await _servicePlayer.GetPlayerInfo(id.Split(',').ToList());
        }
    }
}
