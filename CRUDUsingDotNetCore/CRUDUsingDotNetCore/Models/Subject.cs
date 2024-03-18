﻿using System;
using System.Collections.Generic;

namespace CRUDUsingDotNetCore.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string Name { get; set; } = null!;

    public string Book { get; set; } = null!;

    public string SubjectTeacher { get; set; } = null!;

    public virtual ICollection<Class> ClassNos { get; set; } = new List<Class>();
}
