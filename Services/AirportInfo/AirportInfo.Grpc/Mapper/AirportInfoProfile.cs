using AirportInfo.Grpc.External.Models.AirportApi;
using AirportInfo.Grpc.Protos;
using AutoMapper;

namespace AirportInfo.Grpc.Mapper
{
    public class AirportInfoProfile : Profile
    {
        public AirportInfoProfile()
        {
            CreateMap<Location, LocationModel>();
            CreateMap<AirportInfoData, AirportInfoModel>();
        }
    }
}
