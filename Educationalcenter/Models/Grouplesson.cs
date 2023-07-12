using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Grouplesson
{
    [JsonIgnore]
    public Guid Grouplessonid { get; set; }
    [JsonIgnore]
    public Guid Teacherid { get; set; }

    public int Clientamount { get; set; }

    public decimal Cost { get; set; }
    [JsonIgnore]
    public Guid Subjectid { get; set; }
    [JsonIgnore]
    public virtual ICollection<Grouplessonclient> Grouplessonclients { get; set; } = new List<Grouplessonclient>();
    public virtual ICollection<Groupschedule> Groupschedules { get; set; } = new List<Groupschedule>();

    public virtual Subject Subject { get; set; } = null!;
    [JsonIgnore]
    public virtual Teacher Teacher { get; set; } = null!;
}
