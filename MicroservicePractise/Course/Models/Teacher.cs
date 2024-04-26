using System;
using System.Collections.Generic;

namespace Course.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }
}
