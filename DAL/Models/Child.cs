using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Child
{
    public int ChildId { get; set; }

    public int EmployeeId { get; set; }

    public string ChildName { get; set; } = null!;

    public int ChildAge { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
