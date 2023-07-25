using System;
using System.Collections.Generic;

namespace WB.WebApp.Data;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Price { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
