using System;
using System.Collections.Generic;

namespace Carts.Core.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public virtual BookInventory? Book { get; set; }

    public virtual UserProfile? User { get; set; }
}
