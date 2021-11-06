using Measuring.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Airport.Api.GrpcServices
{
    /// <summary>
    /// Grpc service client for measuring and calulating.
    /// </summary>
    public class MeasuringGrpcService
    {
        private readonly MeasuringProtoService.MeasuringProtoServiceClient _measuringProtoService;

        public MeasuringGrpcService(MeasuringProtoService.MeasuringProtoServiceClient measuringProtoService)
        {
            _measuringProtoService = measuringProtoService ?? throw new ArgumentNullException(nameof(measuringProtoService));
        }

        /// <summary>
        /// Calc distance between two points.
        /// </summary>
        public async Task<DistanceBetweenTwoPointsModel> GetDistanceBetweenTwoPoints(LocationModel from, LocationModel to, DistanceUnit unit)
        {
            var distanceBetweenTwoPointsRequest = new GetDistanceBetweenTwoPointsRequest 
            {
                From = from,
                To = to,
                Unit = unit
            };
            var result = await _measuringProtoService.GetDistanceBetweenTwoPointsAsync(distanceBetweenTwoPointsRequest);
            return result;
        }
    }
}
