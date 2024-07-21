using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class EmpTrigger
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Salary { get; set; }
}
