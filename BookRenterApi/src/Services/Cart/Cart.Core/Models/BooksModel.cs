namespace Carts.Core.Models
{
    public class BooksModel
    {
        public int BookId { get; set; }

        public string BookName { get; set; } = null!;

        public decimal Price { get; set; }

        public string Author { get; set; } = null!;
    }
}
