using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class SeriesGenres
    {
        public Guid SeriesId { get; set; }

        [ForeignKey("SeriesId")]
        public virtual Series Series { get; set; }

        public Guid GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
