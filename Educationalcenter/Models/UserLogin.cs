using System.ComponentModel.DataAnnotations;

namespace Educationalcenter.Models
{
    public class UserLogin
    {
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
