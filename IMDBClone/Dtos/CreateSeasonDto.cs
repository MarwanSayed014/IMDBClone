using IMDBClone.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class CreateSeasonDto
    {
        [Required]
        [MinInt(1800)]
        [CurrentYearMax]
        public int ReleaseYear { get; set; }

        public Guid SeriesId { get; set; }
    }
}
