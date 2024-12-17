using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IMDBClone.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$")]
        public string Password { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string ProfileImgUrl { get; set; } 

        public virtual List<Role> Roles { get; set; }
        public virtual List<SeriesRatings> SeriesRatings { get; set; }
        public virtual List<MovieRatings> MovieRatings { get; set; }

    }
}
