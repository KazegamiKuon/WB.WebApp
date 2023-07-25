using System;
using System.Collections.Generic;

namespace WB.WebApp.Data;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? Dob { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
