using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Models
{
    public class Episode
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(15)]
        [MaxLength(150)]
        public string Description { get; set; }


        [Required]
        [Range(1800, 2050)]
        public int ReleaseYear { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string CoverImgUrl { get; set; }

        public Guid EpisodePrequelId { get; set; }

        [ForeignKey("EpisodePrequelId")]
        public virtual Episode PrequelEpisode { get; set; }

        public Guid SeriesId { get; set; }

        [ForeignKey("SeriesId")]
        public virtual Series Series { get; set; }

        public Guid AdminId { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
    }
}
