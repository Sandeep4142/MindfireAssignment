using System;
using System.Collections.Generic;

namespace CRUDUsingDotNetCore.Models;

public partial class Class
{
    public int ClassNo { get; set; }

    public string ClassTeacher { get; set; } = null!;

    public virtual ICollection<Student> StudentsNavigation { get; set; } = new List<Student>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
