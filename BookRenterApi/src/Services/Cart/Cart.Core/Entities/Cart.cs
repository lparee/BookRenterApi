using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carts.Core.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int? UserId { get; set; }
    [NotMapped]
    public required IList<int> BooksLst { get; set; }
    public virtual ICollection<CartBookMapping> Mappings { get; set; } = new List<CartBookMapping>();
}
