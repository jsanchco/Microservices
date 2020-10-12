using Common.Bus;
using Common.Logging;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OmnichannelDB.API.Config;
using OmnichannelDB.Persistence.Database;
using OmnichannelDB.Service.Queries;
using System.Reflection;

namespace OmnichannelDB.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // Health check            
            // - Add NuGet package: AspNetCore.HealthChecks.UI (Version 3.0.9)
            // - Add folder 'healthchecks' to project
            // - Check UI -> http://localhost:XXXXXX/healthchecks-ui#/healthchecks
            services.AddHealthChecks()
                        .AddCheck("self", () => HealthCheckResult.Healthy())
                        // Add this command bellow to healthcheck DataBaase
                        //.AddDbContextCheck<ApplicationDbContext>(typeof(ApplicationDbContext).Name)
                        ;

            // Event handlers
            services.AddMediatR(Assembly.Load("OmnichannelDB.Service.EventHandlers"));

            // Query services
            services.AddTransient<IPlayerQueryService, PlayerQueryService>();

            services.AddHealthChecksUI();

            services.AddControllers();

            // Receiver bus
            services.AddSingleton<IServiceBus, ServiceBus>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Microservice OmnichannelDB",
                    Description = "OmnichannelDB API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Check url -> https://my.papertrailapp.com/events
            loggerFactory.AddSyslog(
                Configuration.GetValue<string>("Papertrail:host"),
                Configuration.GetValue<int>("Papertrail:port"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI();
            });

            // Run handlers
            app.UseEventHandler();

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}
