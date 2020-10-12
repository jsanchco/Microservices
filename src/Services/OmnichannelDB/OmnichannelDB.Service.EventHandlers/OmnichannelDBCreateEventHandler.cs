using MediatR;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Service.EventHandlers.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace OmnichannelDB.Service.EventHandlers
{
    public class OmnichannelDBCreateEventHandler : INotificationHandler<PlayerInfoCreateCommand>
    {
        private readonly ILogger<OmnichannelDBCreateEventHandler> _logger;

        public OmnichannelDBCreateEventHandler(ILogger<OmnichannelDBCreateEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(PlayerInfoCreateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- New PlayerInfo creation started");
            _logger.LogInformation("--- New PlayerInfo creation ended");
        }
    }
}
