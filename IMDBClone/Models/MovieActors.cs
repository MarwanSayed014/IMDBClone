using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class MovieActors
    {
        public Guid MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public Guid ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }
    }
}
