using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Playtech.Service.Player;
using Playtech.Service.Player.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Playtech.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IServicePlayer _servicePlayer;

        public PlayerController(
            ILogger<PlayerController> logger,
            IServicePlayer servicePlayer
            )
        {
            _logger = logger;
            _servicePlayer = servicePlayer;
        }

        [HttpGet]
        public async Task<PlayerInfo> Get(string tags)
        {
            _logger.LogInformation("In PlayerController -> Get");

            if (string.IsNullOrEmpty(tags))
                return await _servicePlayer.GetPlayerInfo();

            return await _servicePlayer.GetPlayerInfo(tags.Split(',').ToList());
        }
    }
}
