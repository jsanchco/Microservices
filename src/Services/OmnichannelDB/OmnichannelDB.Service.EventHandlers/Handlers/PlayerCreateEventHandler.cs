using MediatR;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Persistence.Database;
using OmnichannelDB.Service.EventHandlers.Commands;
using OmnichannelDB.Service.EventHandlers.Hadlers;
using System.Threading.Tasks;

namespace OmnichannelDB.Service.EventHandlers.Handlers
{
    public class PlayerCreateEventHandler : IHandler<PlayerInfoCreateCommand>
    {
        private readonly ILogger<PlayerCreateEventHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public PlayerCreateEventHandler(
            ILogger<PlayerCreateEventHandler> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Execute(PlayerInfoCreateCommand command)
        {
            await _mediator.Publish(command);
        }

        //public async Task Execute(PlayerInfoCreateCommand command)
        //{
        //    _logger.LogInformation("--- New PlayerInfarted");
        //    //await _context.AddAsync(new Player
        //    //{
        //    //    Firstname = command.Firstname,
        //    //    Lastname = command.Lastname,
        //    //    PersonalId = command.PersonalId,
        //    //    Username = command.Username
        //    //});

        //    //await _context.SaveChangesAsync();
        //    _logger.LogInformation("--- New PlayerInfo creation ended");
        //}
    }
}
