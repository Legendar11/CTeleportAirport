using Airport.Api.GrpcServices;
using Airport.Api.Middlewares;
using AirportInfo.Grpc.Protos;
using Measuring.Grpc.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text.Json;

namespace Airport.Api
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
            services.AddHealthChecks();

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(ValidatorActionFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfiguratorSwagger>();
            services.AddSwaggerGen();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddRouting(options => { options.LowercaseUrls = true; });

            ConfigureGrpc(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Airport.Api v1"));

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        private void ConfigureGrpc(IServiceCollection services)
        {
            var logger = services
                     .BuildServiceProvider()
                     .GetRequiredService<ILogger<ErrorHandlingGrpc>>();

            services.AddGrpcClient<AirportInfoProtoService.AirportInfoProtoServiceClient>(options =>
            {
                var url = Configuration["GrpcSettings:AirportInfoUrl"];
                options.Address = new Uri(url);
                options.Interceptors.Add(new ErrorHandlingGrpc(logger));
            });
            services.AddScoped<AirportInfoGrpcService>();

            services.AddGrpcClient<MeasuringProtoService.MeasuringProtoServiceClient>(options =>
            {
                var url = Configuration["GrpcSettings:MeasuringUrl"];
                options.Address = new Uri(url);
                options.Interceptors.Add(new ErrorHandlingGrpc(logger));
            });
            services.AddScoped<MeasuringGrpcService>();
        }
    }
}
