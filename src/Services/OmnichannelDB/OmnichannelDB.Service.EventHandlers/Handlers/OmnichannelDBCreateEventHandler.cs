using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmnichannelDB.Domain;
using OmnichannelDB.Persistence.Database;
using OmnichannelDB.Service.EventHandlers.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace OmnichannelDB.Service.EventHandlers.Handlers
{
    public class OmnichannelDBCreateEventHandler : INotificationHandler<PlayerInfoCreateCommand>
    {
        private readonly ILogger<OmnichannelDBCreateEventHandler> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OmnichannelDBCreateEventHandler(
            ILogger<OmnichannelDBCreateEventHandler> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Handle(PlayerInfoCreateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- New PlayerInfo creation started");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                await dbContext.AddAsync(new Player
                {
                    Firstname = command.Firstname,
                    Lastname = command.Lastname,
                    PersonalId = command.PersonalId,
                    Username = command.Username
                });

                await dbContext.SaveChangesAsync();
            }
            _logger.LogInformation("--- New PlayerInfo creation ended");
        }
    }
}
