using System;
using System.Collections.Generic;

namespace Educationalcenter;

public partial class Teachersubject
{
    public Guid Teachersubjectid { get; set; }

    public Guid Teacherid { get; set; }

    public Guid Subjectid { get; set; }

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
