using AirportInfo.Grpc.External.Models.AirportApi;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.External.Services.AirportApi
{
    internal interface IAirportApi
    {
        Task<AirportInfoData> GetInfo(string codeByIATA);
    }
}
