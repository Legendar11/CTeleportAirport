using AirportInfo.Grpc.External.Models.AirportApi;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.External.Services.AirportInfoApi
{
    public interface IAirportInfoApi
    {
        Task<AirportInfoData> GetInfo(string codeByIATA);
    }
}
