using System;
using System.Collections.Generic;

namespace StudentService.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
