using System;
using System.Collections.Generic;
using Educationalcenter.Models;
using Newtonsoft.Json;

namespace Educationalcenter;

public partial class Client
{
    [JsonIgnore]
    public Guid Clientid { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }
    [JsonIgnore]
    public Guid Userid { get; set; }

    [JsonIgnore]
    public virtual ICollection<Grouplessonclient> Grouplessonclients { get; set; } = new List<Grouplessonclient>();
    [JsonIgnore]
    public virtual ICollection<Individuallesson> Individuallessons { get; set; } = new List<Individuallesson>();
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
