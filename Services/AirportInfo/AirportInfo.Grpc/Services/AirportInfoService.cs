using AirportInfo.Grpc.External.Services.AirportInfoApi;
using AirportInfo.Grpc.Protos;
using AutoMapper;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.Services
{
    /// <summary>
    /// Implementation of info.proto service.
    /// </summary>
    public class AirportInfoService : AirportInfoProtoService.AirportInfoProtoServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IAirportInfoApi _airportApi;

        public AirportInfoService(IMapper mapper, IAirportInfoApi airportApi)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _airportApi = airportApi ?? throw new ArgumentNullException(nameof(airportApi));
        }

        /// <summary>
        /// Get full information about airport.
        /// </summary>
        public override async Task<AirportInfoModel> GetAirportInfo(GetAirportInfoRequest request, ServerCallContext context)
        {
            var airportData = await _airportApi.GetInfo(request.CodeByIATA);
            var result = _mapper.Map<AirportInfoModel>(airportData);
            return result;
        }
    }
}
