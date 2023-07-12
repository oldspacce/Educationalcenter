using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Educationalcenter;

public partial class Schedule
{
    [JsonIgnore]
    public Guid Scheduleid { get; set; }

    public TimeOnly Time { get; set; }
    [JsonIgnore]
    public virtual ICollection<Scheduledate> Scheduledates { get; set; } = new List<Scheduledate>();
}
