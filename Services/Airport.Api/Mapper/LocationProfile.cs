using AutoMapper;

namespace Airport.Api.Mapper
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<AirportInfo.Grpc.Protos.LocationModel, Measuring.Grpc.Protos.LocationModel>().ReverseMap();
        }
    }
}
