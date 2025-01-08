using IMDBClone.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class CreateSeriesDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name length should be greater than or equal 2")]
        [MaxLength(50, ErrorMessage = "Name length should be less than or equal 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(15, ErrorMessage = "Brief length should be greater than or equal 15")]
        [MaxLength(150, ErrorMessage = "Brief length should be less than or equal 150")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ReleaseYear is required")]
        [MinInt(1800, ErrorMessage = "ReleaseYear should be gteater than or equal 1800")]
        [CurrentYearMax(ErrorMessage = "ReleaseYear should be less than or equal current year")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "ProducerId is required")]
        public Guid ProducerId { get; set; }

        [Required(ErrorMessage = "DirectorId is required")]
        public Guid DirectorId { get; set; }

        [Required(ErrorMessage = "ActorId is required")]
        public Guid ActorId { get; set; }

        [Required(ErrorMessage = "GenreIds is required")]
        [MinLength(1, ErrorMessage = "GenreIds should be gteater than or equal 1")]
        public IEnumerable<Guid> GenreIds { get; set; }


        [Required(ErrorMessage = "File is required")]
        [AllowedExtensions([".png", ".jpg", ".jpeg"], false,
            ErrorMessage = "The file should be in this extensions (png, jpg, jpeg)")]
        [MaxFileSize(5_000_000, false,
            ErrorMessage = "The file should be less than 5 MB")]
        public IFormFile File { get; set; }
    }
}
