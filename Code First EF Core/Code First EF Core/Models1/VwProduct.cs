using System;
using System.Collections.Generic;

namespace Code_First_EF_Core.Models1;

public partial class VwProduct
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }
}
