using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Educationalcenter;

public partial class UserRegistration
{
    [JsonIgnore]
    [Required]
    public Guid Userid { get; set; }
    [Required]
    public string Login { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Surname { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }
    [Required]
    public string? Phone { get; set; }
    public int? Experience { get; set; } = null;
    [Required]
    public string Role { get; set; } = null!;
}
