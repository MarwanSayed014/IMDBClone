using IMDBClone.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class Season
    {
        [Key]
        public Guid Id { get; set; }

        [MinInt(1)]
        public int Number { get; set; }

        [Required]
        [MinInt(1800)]
        [CurrentYearMax]
        public int ReleaseYear { get; set; }

        public Guid SeriesId { get; set; }

        [ForeignKey("SeriesId")]
        public virtual Series Series { get; set; }

        public Guid? SeasonPrequelId { get; set; }

        [ForeignKey("SeasonPrequelId")]
        public virtual Season? PrequelSeason { get; set; }
    }
}
