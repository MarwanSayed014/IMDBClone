using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }
        //public virtual List<User> Users { get; set; } 
    }
}
