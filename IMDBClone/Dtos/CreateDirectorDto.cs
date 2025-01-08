using IMDBClone.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class CreateDirectorDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name length should be greater than 2")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        [RegularExpression("^[a-zA-Z ]*", ErrorMessage = "Name should be characters only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        [MinDate(1950, 1, 1, ErrorMessage = "DateOfBirth should be after 1/1/1950")]
        [NowMaxDate(ErrorMessage = $"DateOfBirth should not be after today")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Brief is required")]
        [MinLength(15, ErrorMessage = "Brief length should be greater than 15")]
        [MaxLength(150, ErrorMessage = "Brief length should be less than 150")]
        public string Brief { get; set; }

        [Required(ErrorMessage = "File is required")]
        [AllowedExtensions([".png", ".jpg", ".jpeg"], false,
            ErrorMessage = "The file should be in this extensions (png, jpg, jpeg)")]
        [MaxFileSize(5_000_000, false,
            ErrorMessage = "The file should be less than 5 MB")]
        public IFormFile File { get; set; }
    }
}
