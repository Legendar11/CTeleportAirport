using AutoMapper;

namespace Airport.Api.Mapper
{
    /// <summary>
    /// Profile for convert one grpc model LocationModel to other grpc model LocationModel.
    /// </summary>
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<AirportInfo.Grpc.Protos.LocationModel, Measuring.Grpc.Protos.LocationModel>().ReverseMap();
        }
    }
}
