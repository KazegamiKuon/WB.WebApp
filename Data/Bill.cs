using System;
using System.Collections.Generic;

namespace WB.WebApp.Data;

public partial class Bill
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ShopId { get; set; }

    public int ProductId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
