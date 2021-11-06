using AirportInfo.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Airport.Api.GrpcServices
{
    /// <summary>
    /// Grpc service client for getting airport information.
    /// </summary>
    public class AirportInfoGrpcService
    {
        private readonly AirportInfoProtoService.AirportInfoProtoServiceClient _airportInfoProtoService;

        public AirportInfoGrpcService(AirportInfoProtoService.AirportInfoProtoServiceClient airportInfoProtoService)
        {
            _airportInfoProtoService = airportInfoProtoService ?? throw new ArgumentNullException(nameof(airportInfoProtoService));
        }

        /// <summary>
        /// Get full airport information.
        /// </summary>
        public async Task<AirportInfoModel> GetAirportInfo(string codeByIATA)
        {
            var airportInfoRequest = new GetAirportInfoRequest { CodeByIATA = codeByIATA };
            var result = await _airportInfoProtoService.GetAirportInfoAsync(airportInfoRequest);
            return result;
        }
    }
}
