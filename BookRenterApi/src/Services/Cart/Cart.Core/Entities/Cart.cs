using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carts.Core.Entities;

public partial class Cart
{
    public int CartId { get; set; }
    [ForeignKey("UserProfile")]
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual ICollection<CartBookMapping> Mappings { get; set; } = new List<CartBookMapping>();
    [NotMapped]
    public IList<int> BooksLst { get; set; } = new List<int>();

}
