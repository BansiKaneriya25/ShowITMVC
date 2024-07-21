using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class EmployeeDetail
{
    public int EmployeeDetailId { get; set; }

    public int? Salary { get; set; }

    public string? Department { get; set; }

    public string? JobType { get; set; }

    public int? EmployeeId { get; set; }

    public int? EmpSeq { get; set; }
}
