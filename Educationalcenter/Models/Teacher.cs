using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Educationalcenter.Models;

namespace Educationalcenter;

public partial class Teacher
{
    [JsonIgnore]
    public Guid Teacherid { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int? Experience { get; set; }

    public string? Phone { get; set; }
    [JsonIgnore]
    public Guid Userid { get; set; }
    [JsonIgnore]
    public virtual ICollection<Dateschedule> Dateschedules { get; set; } = new List<Dateschedule>();
    [JsonIgnore]
    public virtual ICollection<Grouplesson> Grouplessons { get; set; } = new List<Grouplesson>();
    [JsonIgnore]
    public virtual ICollection<Individuallesson> Individuallessons { get; set; } = new List<Individuallesson>();
    [JsonIgnore]
    public virtual ICollection<Teachersubject> Teachersubjects { get; set; } = new List<Teachersubject>();
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
