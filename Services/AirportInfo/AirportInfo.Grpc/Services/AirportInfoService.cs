using AirportInfo.Grpc.External.Services.AirportApi;
using AirportInfo.Grpc.Protos;
using AutoMapper;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.Services
{
    internal class AirportInfoService : AirportInfoProtoService.AirportInfoProtoServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IAirportApi _airportApi;

        public AirportInfoService(IMapper mapper, IAirportApi airportApi)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _airportApi = airportApi ?? throw new ArgumentNullException(nameof(airportApi));
        }

        public override async Task<AirportInfoModel> GetAirportInfo(GetAirportInfoRequest request, ServerCallContext context)
        {
            try
            {
                var airportData = await _airportApi.GetInfo(request.CodeByIATA);
                var result = _mapper.Map<AirportInfoModel>(airportData);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
