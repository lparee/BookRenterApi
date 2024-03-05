using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carts.Core.Entities;

public partial class CartBookMapping
{
    [Key]
    public int CBMappingId { get; set; }
    public int CartId { get; set; } 
    public int BookId { get; set; } 
    public virtual Cart Carts { get; set; } = null!;
    public virtual BookInventory Books { get; set; } = null!;
}
