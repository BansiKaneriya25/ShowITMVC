using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class EmployeeCnd
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string GenderOfPerson { get; set; } = null!;

    public string Department { get; set; } = null!;
}
