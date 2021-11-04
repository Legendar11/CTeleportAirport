using AirportInfo.Grpc.External.Configurations;
using AirportInfo.Grpc.External.Models.AirportApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.External.Services.AirportApi
{
    internal class AirportApi : IAirportApi
    {
        private readonly HttpClient _client;

        private readonly AirportApiConfiguration _configuration;

        private JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AirportApi(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<AirportInfoData> GetInfo(string codeByIATA)
        {
            var url = $"/airports/{codeByIATA}";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<AirportInfoData>(json, JsonSerializerOptions);
            return model;
        }
    }
}
