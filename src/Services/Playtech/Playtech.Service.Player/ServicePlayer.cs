using Common.Bus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Playtech.Domain;
using Playtech.Service.Player.Events;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Playtech.Service.Player
{
    public interface IServicePlayer
    {
        Task<PlayerInfo> GetPlayerInfo(List<string> tags = null);
    }

    public class ServicePlayer : IServicePlayer
    {
        private readonly ILogger<ServicePlayer> _logger;
        private readonly IServiceBus _serviceBus;

        public ServicePlayer(
            ILogger<ServicePlayer> logger, 
            IServiceBus serviceBus)
        {
            _logger = logger;
            _serviceBus = serviceBus;
        }

        public async Task<PlayerInfo> GetPlayerInfo(List<string> tags = null)
        {
            // Response from Service Playtech GetPlayerInfo2
            // Mock ...
            var playerInfoEvent = new PlayerInfoEvent
            {
                Firstname = "Jesus",
                Lastname = "Sanchez",
                Username = "jsanchco",
                PersonaleId = "234353"
            };

            // Azure Service Bus
            _logger.LogInformation("Sending message ...");
            var client = _serviceBus.GetQueueClient("order-stock-update");

            var json = JsonSerializer.Serialize(playerInfoEvent);

            await client.SendAsync(
                new Message(Encoding.UTF8.GetBytes(json))
            );

            await client.CloseAsync();
            _logger.LogInformation("Message sent");

            return new PlayerInfo
            {
                Id = 1,
                Firstname = "Jesus",
                Lastname = "Sanchez",
                Username = "jsanchco",
                PersonaleId = "234353"
            };
        }
    }
}
