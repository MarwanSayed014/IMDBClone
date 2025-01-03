using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IMDBClone.Attributes;

namespace IMDBClone.Models
{
    public class Producer
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name length should be greater than 2")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        [RegularExpression("^[a-zA-Z ]*", ErrorMessage = "Name should be characters only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        [MinDate(1950, 1, 1, ErrorMessage = "DateOfBirth should be after 1/1/1950")]
        [NowMaxDate(ErrorMessage = $"DateOfBirth should not be after today")]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [MinLength(2)]
        public string ProfileImgPath { get; set; }

        [Required(ErrorMessage = "Brief is required")]
        [MinLength(15, ErrorMessage = "Brief length should be greater than 15")]
        [MaxLength(150, ErrorMessage = "Brief length should be less than 150")]
        public string Brief { get; set; }

        public Guid AdminId { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
        //public virtual List<Movie> Movies { get; set; }
        //public virtual List<Series> Serieses { get; set; }
    }
}
