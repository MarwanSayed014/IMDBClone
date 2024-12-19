using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        [Range(1800, 2050)]
        public int ReleaseYear { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string CoverImgUrl { get; set; }

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

        public Guid? SeriesPrequelId { get; set; }

        [ForeignKey("SeriesPrequelId")]
        public virtual Series? PrequelSeries { get; set; }
        //public virtual List<Genre> Genres { get; set; }
        //public virtual List<SeriesRatings> SeriesRatings { get; set; }
        //public virtual List<Episode> Episodes { get; set; }
    }
}
