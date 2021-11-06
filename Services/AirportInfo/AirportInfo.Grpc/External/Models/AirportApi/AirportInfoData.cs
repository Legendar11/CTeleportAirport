using System.Text.Json.Serialization;

namespace AirportInfo.Grpc.External.Models.AirportApi
{
    /// <summary>
    /// Geoposition by degrees;
    /// </summary>
    public sealed record Location
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; init; }

        [JsonPropertyName("lat")]
        public double Latitude { get; init; }
    }
    
    // TODO: Wait .NET 6 and use JsonSerializerContext for faster serialization.
    /// <summary>
    /// Full information about airport.
    /// </summary>
    public sealed record AirportInfoData
    {
        public string Country { get; init; } = string.Empty;

        [JsonPropertyName("city_iata")]
        public string CityIATA { get; init; } = string.Empty;

        public string IATA { get; init; }

        public string City { get; init; } = string.Empty;

        [JsonPropertyName("timezone_region_name")]
        public string TimezoneRegionName { get; init; } = string.Empty;

        [JsonPropertyName("country_iata")]
        public string CountryIATA { get; init; } = string.Empty;

        public int Rating { get; init; }

        public string Name { get; init; } = string.Empty;

        public Location Location { get; init; }

        public string Type { get; init; } = string.Empty;

        public int Hubs { get; init; }
    }
}
