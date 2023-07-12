using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Groupschedule
{
    [JsonIgnore]
    public Guid Groupscheduleid { get; set; }
    [JsonIgnore]
    public Guid Schedulelessonid { get; set; }
    [JsonIgnore]
    public Guid Grouplessonid { get; set; }

    public virtual Grouplesson Grouplesson { get; set; } = null!;

    public virtual Schedulelesson Schedulelesson { get; set; } = null!;
}
