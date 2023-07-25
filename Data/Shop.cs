using System;
using System.Collections.Generic;

namespace WB.WebApp.Data;

public partial class Shop
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
