using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Individualeschedule
{
    [JsonIgnore]
    public Guid Individualschedule { get; set; }
    [JsonIgnore]
    public Guid Schedulelessonid { get; set; }
    [JsonIgnore]
    public Guid Individuallessonid { get; set; }
    [JsonIgnore]
    public virtual Individuallesson Individuallesson { get; set; } = null!;
    public virtual Schedulelesson Schedulelesson { get; set; } = null!;
}
