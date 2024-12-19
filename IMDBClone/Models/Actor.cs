using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class Actor
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string ProfileImgUrl { get; set; }

        [Required]
        [MinLength(15)]
        [MaxLength(150)]
        public string Brief { get; set; }

        public Guid AdminId { get; set; }

        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
        //public virtual List<Movie> Movies { get; set; }
        //public virtual List<Series> Serieses { get; set; }
    }
}
