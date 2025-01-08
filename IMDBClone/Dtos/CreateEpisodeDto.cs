using IMDBClone.Attributes;
using IMDBClone.Models;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class CreateEpisodeDto
    {
        [Required(ErrorMessage = "SeasonId is required")]
        public Guid SeasonId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name length should be greater than 2")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        [RegularExpression("^[a-zA-Z ]*", ErrorMessage = "Name should be characters only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(15, ErrorMessage = "Description length should be greater than 15")]
        [MaxLength(150, ErrorMessage = "Description length should be less than 150")]
        public string Description { get; set; }

        [Required(ErrorMessage = "File is required")]
        [AllowedExtensions([".png", ".jpg", ".jpeg"], false,
            ErrorMessage = "The file should be in this extensions (png, jpg, jpeg)")]
        [MaxFileSize(5_000_000, false,
            ErrorMessage = "The file should be less than 5 MB")]
        public IFormFile File { get; set; }

    }
}
