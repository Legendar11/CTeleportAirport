using Airport.Api.Filters;
using Airport.Api.GrpcServices;
using AirportInfo.Grpc.Protos;
using Measuring.Grpc.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(ValidatorActionFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Airport.Api", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddRouting(options => { options.LowercaseUrls = true; });

            services.AddGrpcClient<AirportInfoProtoService.AirportInfoProtoServiceClient>(options =>
            {
                var url = Configuration["GrpcSettings:AirportInfoUrl"];
                options.Address = new Uri(url);
            });
            services.AddScoped<AirportInfoGrpcService>();

            services.AddGrpcClient<MeasuringProtoService.MeasuringProtoServiceClient>(options =>
            {
                var url = Configuration["GrpcSettings:MeasuringUrl"];
                options.Address = new Uri(url);
            });
            services.AddScoped<MeasuringGrpcService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Airport.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
