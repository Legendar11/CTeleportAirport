namespace Airport.Api.Models.Distance
{
    /// <summary>
    /// Geoposition.
    /// </summary>
    public sealed record Location
    {
        public double Longitude { get; init; }

        public double Latitude { get; init; }
    }

    /// <summary>
    /// Output model for calculation distance between two airports.
    /// </summary>
    public sealed record BetweenOutputModel
    {
        public double Distance { get; init; }

        public Location From { get; init; }

        public Location To { get; init; }
    }
}
