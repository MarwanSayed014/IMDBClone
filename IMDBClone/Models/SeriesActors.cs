using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class SeriesActors
    {
        public Guid SeriesId { get; set; }

        [ForeignKey("SeriesId")]
        public virtual Series Series { get; set; }

        public Guid ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }
    }
}
