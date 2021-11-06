using System.Text.Json.Serialization;

namespace AirportInfo.Grpc.External.Models.AirportApi
{
    /// <summary>
    /// Geoposition by degrees;
    /// </summary>
    public sealed record Location
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }
    
    // TODO: Wait .NET 6 and use JsonSerializerContext for faster serialization.
    /// <summary>
    /// Full information about airport.
    /// </summary>
    public sealed record AirportInfoData
    {
        public string Country { get; set; }

        [JsonPropertyName("city_iata")]
        public string CityIATA { get; set; }

        public string IATA { get; set; }

        public string City { get; set; }

        [JsonPropertyName("timezone_region_name")]
        public string TimezoneRegionName { get; set; }

        [JsonPropertyName("country_iata")]
        public string CountryIATA { get; set; }

        public int Rating { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public string Type { get; set; }

        public int Hubs { get; set; }
    }
}
