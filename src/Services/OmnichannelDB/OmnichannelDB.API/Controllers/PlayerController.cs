using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Service.EventHandlers.Commands;
using OmnichannelDB.Service.Queries;
using OmnichannelDB.Service.Queries.DTOs;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmnichannelDB.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IPlayerQueryService _playerQueryService;
        private readonly IMediator _mediator;

        public PlayerController(
                ILogger<PlayerController> logger,
                IPlayerQueryService playerQueryService,
                IMediator mediator)
        {
            _logger = logger;
            _playerQueryService = playerQueryService;
            _mediator = mediator;
        }

        // players
        [HttpGet]
        public async Task<DataCollection<PlayerDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> products = null;

            if (!string.IsNullOrEmpty(ids))
            {
                products = ids.Split(",").Select(x => Convert.ToInt32(x));
            }

            return await _playerQueryService.GetAllAsync(page, take, products);
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
