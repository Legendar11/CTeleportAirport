using AirportInfo.Grpc.External.Models.AirportApi;
using System.Threading.Tasks;

namespace AirportInfo.Grpc.External.Services.AirportInfoApi
{
    /// <summary>
    /// API for wiorking with airports.
    /// </summary>
    public interface IAirportInfoApi
    {
        /// <summary>
        /// Get info about airport.
        /// </summary>
        /// <see cref="https://en.wikipedia.org/wiki/International_Air_Transport_Association"/>
        /// <param name="codeByIATA">code of airport</param>
        /// <returns></returns>
        Task<AirportInfoData> GetInfo(string codeByIATA);
    }
}
