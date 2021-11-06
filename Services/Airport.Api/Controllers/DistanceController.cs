using Airport.Api.GrpcServices;
using Airport.Api.Models.Distance;
using AutoMapper;
using Measuring.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Airport.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DistanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AirportInfoGrpcService _airportInfoGrpcService;
        private readonly MeasuringGrpcService _measuringGrpcService;

        public DistanceController(
            IMapper mapper,
            AirportInfoGrpcService airportInfoGrpcService,
            MeasuringGrpcService measuringGrpcService
        )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _airportInfoGrpcService = airportInfoGrpcService ?? throw new ArgumentNullException(nameof(airportInfoGrpcService));
            _measuringGrpcService = measuringGrpcService ?? throw new ArgumentNullException(nameof(measuringGrpcService));
        }

        /// <summary>
        /// Get distance between two airports in miles.
        /// </summary>
        [HttpGet("Between")]
        [ProducesResponseType(typeof(BetweenOutputModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BetweenOutputModel>> GetBetween([FromQuery] BetweenInputModel model)
        {
            var fromAirport = await _airportInfoGrpcService.GetAirportInfo(model.From);
            var toAirport = await _airportInfoGrpcService.GetAirportInfo(model.To);

            var fromLocation = _mapper.Map<LocationModel>(fromAirport.Location);
            var toLocation = _mapper.Map<LocationModel>(toAirport.Location);
            var distance = await _measuringGrpcService.GetDistanceBetweenTwoPoints(fromLocation, toLocation, DistanceUnit.Mile);

            var result = _mapper.Map<BetweenOutputModel>(distance);

            return Ok(result);
        }
    }
}
