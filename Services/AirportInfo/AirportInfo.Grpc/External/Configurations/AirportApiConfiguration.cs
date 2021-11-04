using System;

namespace AirportInfo.Grpc.External.Configurations
{
    internal class AirportApiConfiguration
    {
        public string BaseUrl { get; set; }

        public TimeSpan Timeout { get; set; }
    }
}
