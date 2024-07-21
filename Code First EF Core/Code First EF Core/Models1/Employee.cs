using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? Grade { get; set; }

    public bool? IsActive { get; set; }

    public int? EmpSeq { get; set; }

    public string? Gender { get; set; }

    public int? EmpCategoryId { get; set; }

    public virtual EmployeeCategory? EmpCategory { get; set; }
}
