using System.ComponentModel.DataAnnotations;

namespace Airport.Api.Models.Distance
{
    /// <summary>
    /// Input model for calculation distance between two airports.
    /// </summary>
    public sealed record BetweenInputModel
    {
        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string From { get; init; }

        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string To { get; init; }
    }
}
