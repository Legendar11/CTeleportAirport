using Airport.Api.Models.Distance;
using AutoMapper;

namespace Airport.Api.Mapper
{
    public class BetweenOutputProfile : Profile
    {
        public BetweenOutputProfile()
        {
            CreateMap<Measuring.Grpc.Protos.LocationModel, Location>();
            CreateMap<Measuring.Grpc.Protos.DistanceBetweenTwoPointsModel, BetweenOutputModel>();
        }
    }
}
