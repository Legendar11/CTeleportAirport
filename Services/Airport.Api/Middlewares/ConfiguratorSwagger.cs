using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Airport.Api.Middlewares
{
    /// <summary>
    /// Custom swagger configuration for API versioning.
    /// </summary>
    public class ConfiguratorSwagger : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfiguratorSwagger(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            options.OrderActionsBy(orderBy => orderBy.HttpMethod);
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description) => new()
        {
            Title = "Airport Api, version: " + description.GroupName,
            Version = description.ApiVersion.ToString(),
            Description = description.IsDeprecated
                    ? "This API version has been deprecated."
                    : string.Empty,
        };
    }
}
