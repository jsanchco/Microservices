using MediatR;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Domain;
using OmnichannelDB.Persistence.Database;
using OmnichannelDB.Service.EventHandlers.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace OmnichannelDB.Service.EventHandlers
{
    public class OmnichannelDBCreateEventHandler : INotificationHandler<PlayerInfoCreateCommand>
    {
        private readonly ILogger<OmnichannelDBCreateEventHandler> _logger;
        //private readonly ApplicationDbContext _context;

        //public OmnichannelDBCreateEventHandler(
        //    ILogger<OmnichannelDBCreateEventHandler> logger,
        //    ApplicationDbContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}

        public OmnichannelDBCreateEventHandler(
            ILogger<OmnichannelDBCreateEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(PlayerInfoCreateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- New PlayerInfo creation started");
            //await _context.AddAsync(new Player
            //{
            //    Firstname = command.Firstname,
            //    Lastname = command.Lastname,
            //    PersonalId = command.PersonalId,
            //    Username = command.Username
            //});

            //await _context.SaveChangesAsync();
            _logger.LogInformation("--- New PlayerInfo creation ended");
        }
    }
}
