using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class CreateGenreDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name length should be greater than 2")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        [RegularExpression("^[a-zA-Z ]*", ErrorMessage = "Name should be characters only")]
        public string Name { get; set; }
    }
}
