using Carts.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Carts.Core.Models
{
    public  class CartModel
    {
        public int CartId { get; set; }

        public int? UserId { get; set; }

        [CustomValidator.CustomValidator.UniqueProduct(ErrorMessage = "Duplicate books are not allowed")]
        public required IList<int> Books { get; set; }
    }
}
