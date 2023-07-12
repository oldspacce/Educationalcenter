using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Scheduledate
{
    [JsonIgnore]
    public Guid Scheduledateid { get; set; }
    [JsonIgnore]
    public Guid Datescheduleid { get; set; }
    [JsonIgnore]
    public Guid Scheduleid { get; set; }
    public bool Busy { get; set; }
    public virtual Dateschedule Dateschedule { get; set; } = null!;
    public virtual Schedule Schedule { get; set; } = null!;
}
