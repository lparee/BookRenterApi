using System;
using System.Collections.Generic;

namespace Carts.Core.Entities;

public partial class BookInventory
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public decimal Price { get; set; }

    public string Author { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual ICollection<CartBookMapping> Mappings { get; set; } = new List<CartBookMapping>();
}
