using System;

namespace AirportInfo.Grpc.External.Configurations
{
    /// <summary>
    /// Configuration for AirportInfoService HTTP API.
    /// <see cref="AirportInfo.Grpc.Services.AirportInfoService"/>
    /// </summary>
    internal record AirportApiConfiguration
    {
        /// <summary>
        /// URL of api service.
        /// </summary>
        public string BaseUrl { get; init; }

        /// <summary>
        /// Required timeout for http client.
        /// </summary>
        public TimeSpan Timeout { get; init; }
    }
}
