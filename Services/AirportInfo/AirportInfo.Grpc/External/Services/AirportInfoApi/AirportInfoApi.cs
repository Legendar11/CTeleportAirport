using AirportInfo.Grpc.External.Configurations;
using AirportInfo.Grpc.External.Models.AirportApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.External.Services.AirportInfoApi
{
    public class AirportInfoApi : IAirportInfoApi
    {
        private readonly HttpClient _client;

        private JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AirportInfoApi(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<AirportInfoData> GetInfo(string codeByIATA)
        {
            var url = $"/airports/{codeByIATA}";
            var response = await _client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error code {(int)response.StatusCode}, message: {content}");

            var model = JsonSerializer.Deserialize<AirportInfoData>(content, JsonSerializerOptions);
            return model;
        }
    }
}
