using System;
using System.Collections.Generic;

namespace CRUDUsingDotNetCore.Models;

public partial class ErrorLog
{
    public DateTime ErrorDate { get; set; }

    public string ErrorMessage { get; set; } = null!;
}
