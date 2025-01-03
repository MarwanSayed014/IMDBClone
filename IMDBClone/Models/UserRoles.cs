using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class UserRoles
    {
        public Guid UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Guid RoleId { get; set; }
        
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
