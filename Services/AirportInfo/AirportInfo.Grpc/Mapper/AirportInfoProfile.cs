using AirportInfo.Grpc.External.Models.AirportApi;
using AirportInfo.Grpc.Protos;
using AutoMapper;

namespace AirportInfo.Grpc.Mapper
{
    /// <summary>
    /// Profile for convert http api model to grpc model.
    /// </summary>
    public class AirportInfoProfile : Profile
    {
        public AirportInfoProfile()
        {
            CreateMap<Location, LocationModel>();
            CreateMap<AirportInfoData, AirportInfoModel>();
        }
    }
}
