using System;
using System.Collections.Generic;

namespace Carts.Core.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public int Quantity { get; set; }

    public virtual IList<BookInventory> Books { get; set; } = new List<BookInventory>();

    public virtual UserProfile? User { get; set; }
}
