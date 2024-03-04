using Carts.Core.Entities;
using Carts.Data;
using Microsoft.EntityFrameworkCore;

namespace Carts.Data
{
    public interface ICartRepository
    {
        Task<BookInventory?> GetBookByNameAndAuthor(string Name, string Author);
        Task<Cart> AddToCart(Cart cart, string LoginId);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<bool> CheckOut(int Id);
    }

    public class CartRepository : ICartRepository
    {
        private readonly BookRenterDbContext _dbContext;

        public CartRepository(BookRenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookInventory?> GetBookByNameAndAuthor(string Name, string Author)
        {
            return await _dbContext.BooksCollection
                .FirstOrDefaultAsync(p => p.BookName.Equals(Name) && !string.IsNullOrEmpty(Author) ? p.Author.Equals(Author) : true);
        }
        public async Task<Cart> AddToCart(Cart cart, string LoginId)
        {
            var userProfile = _dbContext.UserProfiles.FirstOrDefault(p => p.LoginId == LoginId);
            if (userProfile != null && userProfile.Carts == null)
            {
                userProfile.Carts = new List<Cart>();
                userProfile.Carts.Add(cart);
            }
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            _dbContext.Carts.Update(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> CheckOut(int Id)
        {
            var cart = await _dbContext.Carts.Include(b => b.Books).FirstOrDefaultAsync( c => c.CartId == Id);
            if (cart != null)
            {
                cart.Books.ToList().ForEach(b => b.Quantity = (b.Quantity --));
                _dbContext.Carts.Update(cart);
                _dbContext.Carts.Remove(cart);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
