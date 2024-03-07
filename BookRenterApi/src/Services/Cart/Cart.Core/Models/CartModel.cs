using System.ComponentModel;

namespace Carts.Core.Models
{
    public  class CartModel
    {
        [ReadOnly(true)]
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        [CustomValidator.CustomValidator.UniqueProduct(ErrorMessage = "Duplicate books are not allowed")]
        public IList<int> Books { get; set; }
    }
}
