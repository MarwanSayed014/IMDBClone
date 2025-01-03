using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IMDBClone.Attributes;

namespace IMDBClone.Models
{
    public class Episode
    {
        [Key]
        public Guid Id { get; set; }

        [MinInt(1)]
        public int Number { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z ]*")]
        public string Name { get; set; }

        [Required]
        [MinLength(15)]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [MinLength(2)]
        public string CoverImgPath { get; set; }

        public Guid? EpisodePrequelId { get; set; }

        [ForeignKey("EpisodePrequelId")]
        public virtual Episode? PrequelEpisode { get; set; }

        public Guid SeasonId { get; set; }

        [ForeignKey("SeasonId")]
        public virtual Season Season { get; set; }

        public Guid AdminId { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
    }
}
