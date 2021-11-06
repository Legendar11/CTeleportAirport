namespace Airport.Api.Models.Distance
{
    public sealed record Location
    {
        public double Longitude { get; init; }

        public double Latitude { get; init; }
    }

    public sealed record BetweenOutputModel
    {
        public double Distance { get; init; }

        public Location From { get; init; }

        public Location To { get; init; }
    }
}
