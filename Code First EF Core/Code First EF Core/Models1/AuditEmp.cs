using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class AuditEmp
{
    public int Id { get; set; }

    public int EmpId { get; set; }

    public string OldFirstName { get; set; } = null!;

    public string NewFirstName { get; set; } = null!;

    public string OldLastName { get; set; } = null!;

    public string NewLastName { get; set; } = null!;
}
