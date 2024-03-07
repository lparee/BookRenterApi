using Carts.Core.Entities;
using Carts.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace Carts.Data
{
    public interface ICartRepository
    {
        Task<IEnumerable<BookInventory?>> GetBookByNameAndAuthor(string name, string author);
        Task<IEnumerable<BookInventory?>> GetBookByIds(List<int> bookIds, bool isNotAvailable = false);
        Task<Cart> AddToCart(int bookId, int userId);
        Task<bool> CheckOut(int Id);
        Task<Cart?> GetCartByUserId(int userId);
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
                Where(p => ((!string.IsNullOrEmpty(name) ? p.BookName.ToLower().StartsWith(name.ToLower()) : true)
                && (!string.IsNullOrEmpty(author) ? p.Author.ToLower().StartsWith(author.ToLower()) : true))
                && p.Quantity > 0).ToListAsync();
        }

        public async Task<IEnumerable<BookInventory?>> GetBookByIds(List<int> bookIds, bool isNotAvailable = false)
        {
            return await _dbContext.BooksCollection.
                Where( p => bookIds.Contains(p.BookId) && (isNotAvailable ? p.Quantity < 1 : p.Quantity >= 1)).ToListAsync();
        }

        public async Task<Cart?> GetCartByUserId(int userId)
        {
            return await _dbContext.Carts.Include(c => c.Mappings).Where(d => d.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Cart> AddToCart(int bookId, int userId)
        {
            // Get the user's cart
            var cart = _dbContext.Carts.FirstOrDefault(c => c.UserId == userId);

            // If the user doesn't have a cart yet, create one
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedDate = DateTime.Now
                };
                await _dbContext.Carts.AddAsync(cart);
                _dbContext.SaveChanges();  
            }
            // Create a new cart item
            var cartBookMap = new CartBookMapping
            {
                CartId = cart.CartId,
                BookId = bookId
            };
            await _dbContext.CartBookMapping.AddAsync(cartBookMap);

            _dbContext.SaveChanges();

            cart.Mappings.ToList().AddRange(_dbContext.CartBookMapping.Where(c => c.CartId == cart.CartId).ToList());

            return cart;
        }

        public async Task<bool> CheckOut(int Id)
        {
            var cart = await _dbContext.Carts.Include(b => b.Mappings).FirstOrDefaultAsync(c => c.CartId == Id);
            if (cart != null)
            {
                //getting refrenced books collection from cart mapping
                var book = _dbContext.BooksCollection.Where(b => cart.Mappings.Select(m => m.BookId).Contains(b.BookId));

                //reducing book quantity to one                
                book.ToList().ForEach(b => b.Quantity -= 1);

                _dbContext.BooksCollection.UpdateRange(book.ToList());

                _dbContext.CartBookMapping.RemoveRange(cart.Mappings);

                _dbContext.Carts.Remove(cart);

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
