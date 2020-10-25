using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace OcelotGw.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.  
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940  
        public void ConfigureServices(IServiceCollection services)
        {
            // Authentication
            //var authenticationProviderKey = "TestKey";
            //Action<IdentityServerAuthenticationOptions> opt = o =>
            //{
            //    o.Authority = "http://localhost:5001";
            //    o.ApiName = "SampleService";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.RequireHttpsMetadata = false;
            //};

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //    .AddIdentityServerAuthentication(authenticationProviderKey, opt);

            //services.AddAuthentication()
            //    .AddJwtBearer("TestKey", x =>
            //    {
            //        x.Authority = "http://localhost:5001";
            //        x.RequireHttpsMetadata = false;
            //        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //        {
            //            ValidateAudience = false
            //        };
            //    });

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = CreateTokenValidationParameters();
                });
            // Authentication

            services.AddOcelot();

            services.AddSwaggerForOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();
            app.UseSwaggerForOcelotUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            await app.UseOcelot();           
        }

        private TokenValidationParameters CreateTokenValidationParameters() //we ignore token validation because gateway validates it
        {
            var result = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = false,
                SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                {
                    var jwt = new JwtSecurityToken(token);
                    return jwt;
                },
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
            result.RequireSignedTokens = false;
            return result;
        }
    }
}
