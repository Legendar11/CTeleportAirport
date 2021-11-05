using Measuring.Grpc.Protos;
using Measuring.Grpc.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Measuring.Tests.MeasuringServiceTest
{
    [Category("Module")]
    public class Tests
    {
        private MeasuringProtoService.MeasuringProtoServiceBase Service { get; set; }

        [SetUp]
        public void Setup()
        {
            Service = new MeasuringService();
        }

        [Test]
        [Order(1)]
        [Description("Initialization")]
        public void IsServiceExist()
        {
            Assert.IsNotNull(Service);
        }

        [Test]
        [Order(2)]
        [Description("Invalid locations")]
        [TestCaseSource(nameof(GetInvalidParametrs))]
        public void IncorrectParametrs((LocationModel from, LocationModel to) input)
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => 
                await Service.GetDistanceBetweenTwoPoints(new GetDistanceBetweenTwoPointsRequest
            {
                From = input.from,
                To = input.to,
                Unit = DistanceUnit.Mile
            }, null));
        }

        [Test]
        [Order(3)]
        [Description("Equal locations")]
        [TestCaseSource(nameof(GetEqualParametrs))]
        public async Task EqualParametrs((LocationModel from, LocationModel to) input)
        {
            foreach (DistanceUnit distanceUnit in Enum.GetValues(typeof(DistanceUnit)))
            {
                var result = await Service.GetDistanceBetweenTwoPoints(new GetDistanceBetweenTwoPointsRequest
                {
                    From = input.from,
                    To = input.to,
                    Unit = distanceUnit
                }, null);
                Assert.AreEqual(result.Distance, 0.0);
            }
        }

        [Test]
        [Order(4)]
        [Description("Calc real distance")]
        [TestCaseSource(nameof(GetRealParametrs))]
        public async Task RealParametrs((LocationModel from, LocationModel to, double expected) input)
        {
            var resultTo = await Service.GetDistanceBetweenTwoPoints(new GetDistanceBetweenTwoPointsRequest
            {
                From = input.from,
                To = input.to,
                Unit = DistanceUnit.Mile
            }, null);
            Assert.AreEqual(Math.Round(resultTo.Distance, 3), Math.Round(input.expected, 3));

            var resultFrom = await Service.GetDistanceBetweenTwoPoints(new GetDistanceBetweenTwoPointsRequest
            {
                From = input.to,
                To = input.from,
                Unit = DistanceUnit.Mile
            }, null);
            Assert.AreEqual(Math.Round(resultFrom.Distance, 3), Math.Round(input.expected, 3));
        }

        #region Testcase Sources
        public static IEnumerable<(LocationModel, LocationModel)> GetInvalidParametrs()
        {
            var incorrectDoubles = new double[]
            {
                double.NaN,
                double.PositiveInfinity,
                double.NegativeInfinity,
                double.MaxValue,
                double.MinValue,
                181,
                -181
            };

            foreach (var incorrectDouble in incorrectDoubles)
            {
                yield return (
                    new LocationModel { Latitude = incorrectDouble, Longitude = 0 },
                    new LocationModel { Latitude = 0, Longitude = 0 });

                yield return (
                    new LocationModel { Latitude = 0, Longitude = 0 },
                    new LocationModel { Latitude = incorrectDouble, Longitude = 0 });

                yield return (
                    new LocationModel { Latitude = 0, Longitude = incorrectDouble },
                    new LocationModel { Latitude = 0, Longitude = 0 });

                yield return (
                    new LocationModel { Latitude = 0, Longitude = 0 },
                    new LocationModel { Latitude = 0, Longitude = incorrectDouble });

                yield return (
                    new LocationModel { Latitude = incorrectDouble, Longitude = incorrectDouble },
                    new LocationModel { Latitude = incorrectDouble, Longitude = incorrectDouble });
            }
        }

        public static IEnumerable<(LocationModel, LocationModel)> GetEqualParametrs()
        {
            yield return (
                   new LocationModel { Latitude = 0, Longitude = 0 },
                   new LocationModel { Latitude = 0, Longitude = 0 });

            yield return (
                   new LocationModel { Latitude = 1, Longitude = 1 },
                   new LocationModel { Latitude = 1, Longitude = 1 });
        }

        public static IEnumerable<(LocationModel, LocationModel, double)> GetRealParametrs()
        {
            yield return (
                new LocationModel { Latitude = 42.787281, Longitude = 141.681341 },
                new LocationModel { Latitude = 45.457715, Longitude = -73.749906 },
                5961.7877);

            yield return (
                new LocationModel { Latitude = -8.5, Longitude = 161.25 },
                new LocationModel { Latitude = -19.283333, Longitude = 48.833333 },
                7454.0713);

            yield return (
                new LocationModel { Latitude = -17, Longitude = 145.466667 },
                new LocationModel { Latitude = -19.283333, Longitude = 48.833333 },
                6248.6495);

            yield return (
                new LocationModel { Latitude = -6.583333, Longitude = 144.666667 },
                new LocationModel { Latitude = -30.5, Longitude = 27.6 },
                7554.7944);

            yield return (
                new LocationModel { Latitude = 34.733805, Longitude = -120.575367 },
                new LocationModel { Latitude = 24.697778, Longitude = -77.796111 },
                2637.8349);
        }
        #endregion Testcase Sources
    }
}