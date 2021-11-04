using Airport.Api.DTO.Api.Distance;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        /// <summary>
        /// Geoposition coordinates.
        /// </summary>
        /// <param name="Latitude">Latitude coordinate.</param>
        /// <param name="Longitude">Longitude coordinate.</param>
        public record GeoPosition(
            double Latitude,
            double Longitude
        );

        public enum DistanceUnit
        {
            Mile,
            NauticalMile,
            Kilometr
        }

        private class GreatCircleCalculator
        {
            private double DegreesToRadians(double deg) => deg * Math.PI / 180.0;

            private double rad2deg(double rad) => rad / Math.PI * 180.0;

            public double Distance(GeoPosition from, GeoPosition to, DistanceUnit unit)
            {
                if (from == to)
                    return 0;

                var theta = from.Longitude - to.Longitude;
                var dist = Math.Sin(DegreesToRadians(from.Latitude)) * Math.Sin(DegreesToRadians(to.Latitude)) 
                    + Math.Cos(DegreesToRadians(from.Latitude)) * Math.Cos(DegreesToRadians(to.Latitude)) * Math.Cos(DegreesToRadians(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;

                dist = unit switch
                {
                    DistanceUnit.NauticalMile => dist * 0.8684,
                    DistanceUnit.Kilometr => dist * 1.609344,
                    _ => dist
                };

                return dist;
            }
        }

        [HttpGet("Between")]
        public async Task<ActionResult<string>> GetBetween([FromQuery] BetweenInputModel model)
        {
            var calc = new GreatCircleCalculator();

            var f = calc.Distance(new GeoPosition(32.309069, 4.763385), new GeoPosition(52.309069, 2.763385), DistanceUnit.Kilometr);

            var d = distance(32.309069, 4.763385, 52.309069, 2.763385,  'K');

            return Ok("1");
        }
    }
}
