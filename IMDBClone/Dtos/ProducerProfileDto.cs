using IMDBClone.Models;

namespace IMDBClone.Dtos
{
    public class ProducerProfileDto
    {
        public Producer Producer { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Series> Serieses { get; set; }
    }
}
