using System.ComponentModel.DataAnnotations;

namespace Airport.Api.DTO.Api.Distance
{
    public class BetweenInputModel
    {
        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string From { get; set; }

        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string To { get; set; }
    }
}
