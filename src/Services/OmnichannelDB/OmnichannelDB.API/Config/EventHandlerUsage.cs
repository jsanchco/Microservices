using Common.Bus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using OmnichannelDB.Service.EventHandlers.Commands;
using OmnichannelDB.Service.EventHandlers.Hadlers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OmnichannelDB.API.Config
{
    public static class EventHandlerUsage
    {
        public static void UseEventHandler(this IApplicationBuilder app)
        {
            var receiver = app.ApplicationServices.GetService<IServiceBus>();

            // Handlers
            //var playerInfoEventHandler = app.ApplicationServices.GetService<INotificationHandler<PlayerInfoCreateCommand>>();
            var playerInfoEventHandler = app.ApplicationServices.GetService<IHandler<PlayerInfoCreateCommand>>();

            Register(receiver, "order-stock-update", playerInfoEventHandler);
        }

        private static void Register<T>(
             IServiceBus service,
             string queue,
             IHandler<T> handler) where T : class
        {
            var client = service.GetQueueClient(queue);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            client.RegisterMessageHandler(async (Message message, CancellationToken token) => {
                var payload = JsonSerializer.Deserialize<T>(
                    Encoding.UTF8.GetString(message.Body)
                );

                await client.CompleteAsync(message.SystemProperties.LockToken);
                await handler.Execute(payload);
            }, messageHandlerOptions);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            System.Diagnostics.Debug.WriteLine(exceptionReceivedEventArgs.Exception.Message);

            // your custom message log
            return Task.CompletedTask;
        }
    }
}
