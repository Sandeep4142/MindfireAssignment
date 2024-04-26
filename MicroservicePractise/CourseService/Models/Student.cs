using System;
using System.Collections.Generic;

namespace CourseService.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
