using Airport.Api.Models.Distance;
using AutoMapper;

namespace Airport.Api.Mapper
{
    /// <summary>
    /// Profile for convert grpc model to http api model.
    /// </summary>
    public class BetweenOutputProfile : Profile
    {
        public BetweenOutputProfile()
        {
            CreateMap<Measuring.Grpc.Protos.LocationModel, Location>();
            CreateMap<Measuring.Grpc.Protos.DistanceBetweenTwoPointsModel, BetweenOutputModel>();
        }
    }
}
