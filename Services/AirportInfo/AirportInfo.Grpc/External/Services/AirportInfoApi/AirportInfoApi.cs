using AirportInfo.Grpc.External.Models.AirportApi;
using AirportInfo.Grpc.JsonConverters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.External.Services.AirportInfoApi
{
    public class AirportInfoApi : IAirportInfoApi
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _cache;

        public AirportInfoApi(HttpClient client, IMemoryCache cache)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<AirportInfoData> GetInfo(string codeByIATA)
        {
            if (!_cache.TryGetValue(codeByIATA, out string content))
            {
                var url = $"/airports/{codeByIATA}";
                var response = await _client.GetAsync(url);

                content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed try download info about {codeByIATA}, reason: {content}", null, response.StatusCode);

                _cache.Set(codeByIATA, content, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
            }

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            jsonSerializerOptions.Converters.Add(new EmptyStringConverter());
            var model = JsonSerializer.Deserialize<AirportInfoData>(content, jsonSerializerOptions);

            return model;
        }
    }
}
