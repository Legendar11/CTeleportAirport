using AirportInfo.Grpc.External.Models.AirportApi;
using AirportInfo.Grpc.External.Services.AirportInfoApi;
using AirportInfo.Grpc.Mapper;
using AirportInfo.Grpc.Protos;
using AirportInfo.Grpc.Services;
using AutoMapper;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AirportInfo.Tests.AirportInfoServiceTest
{
    [Category("Module")]
    public class Tests
    {
        private AirportInfoProtoService.AirportInfoProtoServiceBase Service { get; set; }

        private IAirportInfoApi Api { get; set; }

        private IMapper Mapper { get; set; }

        const string defaultResponse = @"
            {
                ""country"":""Japan"",
                ""city_iata"":""SPK"",
                ""iata"":""CTS"",
                ""city"":""Sapporo"",
                ""timezone_region_name"":""Asia / Tokyo"",
                ""country_iata"":""JP"",
                ""rating"":1,
                ""name"":""New Chitose"",
                ""location"":{""lon"":141.681341,""lat"":42.787281},
                ""type"":""airport"",
                ""hubs"":0
            }";

        [SetUp]
        public void Setup()
        {
            
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(defaultResponse),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new System.Uri("http://test.com")
            };

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AirportInfoProfile>());
            Mapper = config.CreateMapper();

            Api = new AirportInfoApi(httpClient);

            Service = new AirportInfoService(Mapper, Api);
        }

        [Test]
        [Order(1)]
        [Description("Initialization")]
        public void IsServiceExist()
        {
            Assert.IsNotNull(Api);
            Assert.IsNotNull(Service);
        }

        [Test]
        [Order(2)]
        [Description("Check automapper profile")]
        public void AutomapperProfile()
        {
            var model = JsonSerializer.Deserialize<AirportInfoData>(defaultResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var mapped = Mapper.Map<AirportInfoModel>(model);
            Assert.IsNotNull(mapped);
        }

        [Test]
        [Order(3)]
        [Description("Determine working GetAirportInfo")]
        public void IsApiWorking()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var result = await Service.GetAirportInfo(new GetAirportInfoRequest
                {
                    CodeByIATA = "ATS"
                }, null);
                Assert.IsNotNull(result);
            });
        }
    }
}