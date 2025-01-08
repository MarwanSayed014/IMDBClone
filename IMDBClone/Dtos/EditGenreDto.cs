using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class EditGenreDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name length should be greater than 2")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        public string Name { get; set; }
    }
}
