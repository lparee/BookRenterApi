using Carts.Core.Entities;
using Carts.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Carts.Data
{
    public interface ICartRepository
    {
        Task<IEnumerable<BookInventory?>> GetBookByNameAndAuthor(string name, string author);
        Task<Cart> AddToCart(Cart cart, int userId);
        Task<Cart> UpdateCart(Cart cart);
        Task<bool> CheckOut(int Id);
    }

    public class CartRepository : ICartRepository
    {
        private readonly BookRenterDbContext _dbContext;

        public CartRepository(BookRenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookInventory?>> GetBookByNameAndAuthor(string name, string author)
        {
            return await _dbContext.BooksCollection.
                Where(p => p.BookName.Equals(name) || p.Author.Equals(author)).ToListAsync();
        }
        public async Task<Cart> AddToCart(Cart cart, int userId)
        {
            _dbContext.Add(cart);

            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> UpdateCart(Cart cart)
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
