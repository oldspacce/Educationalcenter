using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Grouplessonclient
{
    [JsonIgnore]
    public Guid Grouplessonclientid { get; set; }
    [JsonIgnore]
    public Guid Clientid { get; set; }
    [JsonIgnore]
    public Guid Grouplessonid { get; set; }
    public virtual Client Client { get; set; } = null!;

    public virtual Grouplesson Grouplesson { get; set; } = null!;
}
