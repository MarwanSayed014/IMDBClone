using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name length should be greater than 2")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        [RegularExpression("^[a-zA-Z ]*", ErrorMessage = "Name should be characters only")]
        public string Name { get; set; }

        public Guid AdminId { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
        //public virtual List<Series> Serieses { get; set; }
        //public virtual List<Movie> Movies { get; set; }
    }
}
