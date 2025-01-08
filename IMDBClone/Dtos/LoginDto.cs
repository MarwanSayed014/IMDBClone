using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Dtos
{
    public class LoginDto
    {
        [MinLength(2)]
        [MaxLength(50)]
        public required string UserName { get; set; }

        [MaxLength(50)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$")]
        public required string Password { get; set; }
    }
}
