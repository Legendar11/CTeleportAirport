using AirportInfo.Grpc.External.Configurations;
using AirportInfo.Grpc.External.Services.AirportApi;
using AirportInfo.Grpc.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AirportInfo.Grpc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpClient<IAirportApi, AirportApi>("AirportApi", client =>
            {
                const string sectionName = "AirportApiSettings";
                var configuration = Configuration
                    .GetSection(sectionName)
                    .Get<AirportApiConfiguration>()
                    ?? throw new ArgumentNullException(sectionName);

                client.BaseAddress = new Uri(configuration.BaseUrl);
                client.Timeout = configuration.Timeout;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AirportInfoService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
