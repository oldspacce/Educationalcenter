using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Educationalcenter;

public partial class Subject
{
    [JsonIgnore]
    public Guid Subjectid { get; set; }

    public string Subjectname { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Grouplesson> Grouplessons { get; set; } = new List<Grouplesson>();
    [JsonIgnore]
    public virtual ICollection<Teachersubject> Teachersubjects { get; set; } = new List<Teachersubject>();
}
