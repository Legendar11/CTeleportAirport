using AirportInfo.Grpc.Protos;
using AirportInfo.Grpc.Services;
using NUnit.Framework;

namespace AirportInfo.Tests.AirportInfoServiceTest
{
    [Category("Module")]
    public class Tests
    {
        private AirportInfoProtoService.AirportInfoProtoServiceBase Service { get; set; }

        [SetUp]
        public void Setup()
        {
            Service = new AirportInfoService(null, null);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}