using Grpc.Core;
using Measuring.Grpc.Extensions;
using Measuring.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Measuring.Grpc.Services
{
    public class MeasuringService : MeasuringProtoService.MeasuringProtoServiceBase
    {
        public override async Task<DistanceBetweenTwoPointsModel> GetDistanceBetweenTwoPoints(GetDistanceBetweenTwoPointsRequest request, ServerCallContext context)
        {
            // https://en.wikipedia.org/wiki/Haversine_formula

            var from = request.From;
            var to = request.To;

            var result = new DistanceBetweenTwoPointsModel
            {
                From = from,
                To = to
            };

            if (from == to)
            {
                result.Distance = 0;
                return await Task.FromResult(result);
            }

            var theta = from.Longitude - to.Longitude;
            result.Distance = from.Latitude.DegreesToRadians().Sin() * to.Latitude.DegreesToRadians().Sin()
                + from.Latitude.DegreesToRadians().Cos() * to.Latitude.DegreesToRadians().Cos() * theta.DegreesToRadians().Cos();
            result.Distance = Math.Acos(result.Distance).RadiansToDegrees() * 60 * 1.1515;

            const double milesToKilometr = 1.609344;
            const double milesToNauticalMile = 0.8684;

            result.Distance = request.Unit switch
            {
                DistanceUnit.NauticalMile => result.Distance * milesToNauticalMile,
                DistanceUnit.Kilometr => result.Distance * milesToKilometr,
                _ => result.Distance
            };

            return await Task.FromResult(result);
        }
    }
}
