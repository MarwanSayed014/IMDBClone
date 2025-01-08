using IMDBClone.Models;

namespace IMDBClone.Dtos
{
    public class ActorProfileDto
    {
        public Actor Actor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Series> Serieses { get; set; }
    }
}
