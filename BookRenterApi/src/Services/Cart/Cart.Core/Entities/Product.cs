using System;
using System.Collections.Generic;

namespace Carts.Core.Entities;

public partial class BookInventory
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public decimal Price { get; set; }

    public string Author { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int Quantity { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }
}
