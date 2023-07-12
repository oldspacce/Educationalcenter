using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Dateschedule
{
    [JsonIgnore]
    public Guid Datescheduleid { get; set; }
    [JsonIgnore]
    public Guid Teacherid { get; set; }

    public DateOnly Date { get; set; }
    [JsonIgnore]
    public virtual ICollection<Scheduledate> Scheduledates { get; set; } = new List<Scheduledate>();
    [JsonIgnore]
    public virtual Teacher Teacher { get; set; } = null!;
}
