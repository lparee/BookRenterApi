using System;
using System.Collections.Generic;

namespace Carts.Core.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<BookInventory> Books { get; set; } = new List<BookInventory>();
}
