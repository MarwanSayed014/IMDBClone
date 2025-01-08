using IMDBClone.Models;

namespace IMDBClone.Dtos
{
    public class MovieProfileDto
    {
        public Movie Movie { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public Producer Producer { get; set; }
        public Director Director { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }
}
