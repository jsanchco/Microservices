using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Service.EventHandlers.Commands;
using OmnichannelDB.Service.Queries;
using OmnichannelDB.Service.Queries.DTOs;
using OmnichannelDB.Service.Queries.Filters;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmnichannelDB.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayerQueryService _playerQueryService;
        private readonly IMediator _mediator;

        public PlayersController(
                ILogger<PlayersController> logger,
                IPlayerQueryService playerQueryService,
                IMediator mediator)
        {
            _logger = logger;
            _playerQueryService = playerQueryService;
            _mediator = mediator;
        }

        // players
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterPlayers filterPlayers)
        {
            var result = await _playerQueryService.GetAllWithFilterAsync(filterPlayers);

            return Ok(result);
        }

        // players/1
        [HttpGet("{id}")]
        public async Task<PlayerDto> Get(int id)
        {
            return await _playerQueryService.GetAsync(id);
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
