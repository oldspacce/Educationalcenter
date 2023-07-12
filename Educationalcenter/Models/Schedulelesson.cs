using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Schedulelesson
{
    [JsonIgnore]
    public Guid Schedulelessonid { get; set; }
    public TimeOnly Starttime { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Endtime { get; set; }
    [JsonIgnore]
    public virtual ICollection<Groupschedule> Groupschedules { get; set; } = new List<Groupschedule>();
    [JsonIgnore]
    public virtual ICollection<Individualeschedule> Individualeschedules { get; set; } = new List<Individualeschedule>();
}
