using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Individuallesson
{
    [JsonIgnore]
    public Guid Individuallessonid { get; set; }
    [JsonIgnore]
    public Guid Teacherid { get; set; }
    [JsonIgnore]
    public Guid Clientid { get; set; }

    public decimal Cost { get; set; }
    [JsonIgnore]
    public virtual Client Client { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Individualeschedule> Individualeschedules { get; set; } = new List<Individualeschedule>();
    [JsonIgnore]
    public virtual Teacher Teacher { get; set; } = null!;
}
