using IMDBClone.Attributes;
using IMDBClone.Models;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class EditSeasonDto
    {
        public Guid seriesId { get; set; }

        [Required]
        [MinInt(1800)]
        [CurrentYearMax]
        public int ReleaseYear { get; set; }
    }
}
