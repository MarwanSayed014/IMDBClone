using IMDBClone.Models;

namespace IMDBClone.Dtos
{
    public class DirectorProfileDto
    {
        public Director Director { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Series> Serieses { get; set; }
    }
}
