using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Educationalcenter.Models
{
    public class User
    {
        [JsonIgnore]
        [Required]
        public Guid Userid { get; set; }
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
        [JsonIgnore]
        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
