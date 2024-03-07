using Carts.Core.Entities;
using Carts.Core.Models;
using Carts.Data;

namespace Carts.Service
{
    public interface ICartService
    {
        Task<IEnumerable<BooksModel>> GetBookByNameAndAuthor(string name, string author);
        Task<IEnumerable<BooksModel>> GetBookByIds(List<int> bookIds, bool isNotAvailable = false);
        Task<CartModel> AddToCart(int bookId, int userId);
        Task<bool> CheckOut(int cartId);
        Task<CartModel> GetCartByUserId(int userId);
    }

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartModel> AddToCart(int bookId, int userId)
        {
            //var cartEntity = MapToCartEntity(cart);
            var addedcart =  await _cartRepository.AddToCart(bookId, userId);
                return MapToCartModel(addedcart);
        }

        public async Task<CartModel> GetCartByUserId(int userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            return  MapToCartModel(cart);
        }

        public async Task<bool> CheckOut(int cartId)
        {
            return await _cartRepository.CheckOut(cartId);
        }

        public async Task<IEnumerable<BooksModel>> GetBookByNameAndAuthor(string name, string author)
        {
            var books = await _cartRepository.GetBookByNameAndAuthor(name, author);
            return MapToBookModel(books);
        }

        public async Task<IEnumerable<BooksModel>> GetBookByIds(List<int> bookIds, bool isNotAvailable = false)
        {
            var books = await _cartRepository.GetBookByIds(bookIds, isNotAvailable);
            return MapToBookModel(books);
        }
        private static IEnumerable<BooksModel> MapToBookModel(IEnumerable<Carts.Core.Entities.BookInventory?> book)
        {
            List<BooksModel> tempbookmodel = new List<BooksModel>();

            if (book == null || !book.Any())
                return tempbookmodel;

            book.ToList().ForEach(b => tempbookmodel.Add(new BooksModel
            {
                BookId = b.BookId,
                Author = b.Author,
                BookName = b.BookName,
                Price = b.Price
            }
            ));
            return tempbookmodel;
        }
        private static CartModel MapToCartModel(Carts.Core.Entities.Cart? cart)
        {
            if (cart != null)
                return new CartModel
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    Books = cart.Mappings.Select(c => c.BookId).ToList()
                };
            else
                return new CartModel();
        }

        private static Carts.Core.Entities.Cart MapToCartEntity(CartModel cartModel)
        {
            List<CartBookMapping> mappings = new List<CartBookMapping>();
            return new Carts.Core.Entities.Cart
            {
                UserId = cartModel.UserId,
                BooksLst = cartModel.Books
            };
        }
    }
}
