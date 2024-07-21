using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class VwTable
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? Grade { get; set; }

    public bool IsActive { get; set; }

    public int? EmpSeq { get; set; }

    public string? Salary { get; set; }

    public string? Department { get; set; }

    public string? JobType { get; set; }
}
