using System;
using System.Collections.Generic;

namespace CRUDUsingDotNetCore.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public int ClassNo { get; set; }

    public int AdmissionYear { get; set; }

    public virtual Class ClassNoNavigation { get; set; } = null!;

    public virtual ICollection<Class> ClassNos { get; set; } = new List<Class>();
}
