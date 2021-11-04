using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Airport.Api.Models.Distance
{
    public sealed record BetweenInputModel
    {
        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string From { get; set; }

        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string To { get; set; }
    }
}
