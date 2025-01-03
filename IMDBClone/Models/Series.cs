using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IMDBClone.Attributes;

namespace IMDBClone.Models
{
    public class Series
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
        [MinInt(1800)]
        [CurrentYearMax()]
        public int ReleaseYear { get; set; }

        [Required]
        [MinLength(2)]
        public string CoverImgPath { get; set; }

        [Range(0, 10)]
        public double TotalRating { get; set; }

        public Guid AdminId { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }

        public Guid ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public virtual Producer Producer { get; set; }

        public Guid DirectorId { get; set; }

        [ForeignKey("DirectorId")]
        public virtual Director Director { get; set; }
        public Guid ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }


        //public virtual List<Genre> Genres { get; set; }
        //public virtual List<SeriesRatings> SeriesRatings { get; set; }
        //public virtual List<Episode> Episodes { get; set; }
    }
}
