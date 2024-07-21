using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class EmployeeCategory
{
    public int EmpCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
