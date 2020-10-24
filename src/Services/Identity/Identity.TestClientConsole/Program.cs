using Identity.TestClientConsole.Logger;
using Identity.TestClientConsole.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identity.TestClientConsole
{
    class Program
    {
        static async Task Main()
        {
            var cfg = InitOptions<AppConfig>();

            var logger = new LoggerInFile();
            await logger.ConfigureLogger();
            logger.Init();

            await logger.WriteLogger($"Start process ...");

            await Test(cfg, logger);

            await logger.WriteLogger($"");
            await logger.WriteLogger($"Press any key to exit ...");

            Console.ReadLine();
        }

        private async static Task Test(AppConfig cfg, LoggerInFile logger)
        {
            var client = new HttpClient();
            var discoveryDocument = await client.GetDiscoveryDocumentAsync(cfg.Identity.url);

            await logger.WriteLogger($"Get DiscoveryDocument ...");
            if (discoveryDocument.IsError)
            {
                await logger.WriteLogger($"Error in Get DiscoveryDocument", LogEventLevel.ERR);
                return;
            }
            await logger.WriteLogger($"DiscoveryDocument OK!!");

            await logger.WriteLogger($"");

            await logger.WriteLogger($"Get tokenResponse ...");
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = cfg.Identity.clientId,
                ClientSecret = cfg.Identity.secret,
                Scope = cfg.Identity.scope
            });

            if (tokenResponse.IsError)
            {
                await logger.WriteLogger($"Error in RequestClientCredentialsTokenAsync: Reason -> {tokenResponse.HttpErrorReason}, Error -> {tokenResponse.Error}");
                return;
            }
            await logger.WriteLogger($"tokenResponse: {tokenResponse.Json}");

            await logger.WriteLogger($"");

            // call api
            await logger.WriteLogger($"Call API");
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            try
            {
                var response = await apiClient.GetAsync(cfg.API.url);
                if (!response.IsSuccessStatusCode)
                {
                    await logger.WriteLogger($"Error in Get: StatusCode -> {response.StatusCode},  Reason -> {response.ReasonPhrase} ", LogEventLevel.ERR);
                    return;
                }

                var content = await response.Content.ReadAsStringAsync();
                //await logger.WriteLogger($"Response -> {JArray.Parse(content)}");
                await logger.WriteLogger($"Response -> {content}");

            }
            catch (Exception ex)
            {
                await logger.WriteLogger($"Error in Get: {ex.Message}", LogEventLevel.ERR);
                return;
            }
        }

        #region Add appSettings.json
        private static T InitOptions<T>() where T : new()
        {
            var config = InitConfig();
            return config.Get<T>();
        }

        private static IConfigurationRoot InitConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
        #endregion
    }
}
