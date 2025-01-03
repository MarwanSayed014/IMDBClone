using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class MovieGenres
    {
        public Guid MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public Guid GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
