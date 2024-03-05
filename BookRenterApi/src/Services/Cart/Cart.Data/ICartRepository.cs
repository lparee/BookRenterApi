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
                Where(p => p.BookName.Equals(name) || p.Author.Equals(author)).ToListAsync();
        }

        public async Task<Cart?> GetCartByUserId(int userId)
        {
            return await _dbContext.Carts.Include(c => c.Mappings).Where(d => d.UserId == userId).FirstOrDefaultAsync();
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
            //var query = from _cart in _dbContext.Carts
            //            join _map in _dbContext.CartBookMapping on _cart.CartId equals _map.CartId
            //            join _book in _dbContext.BooksCollection on _map.BookId equals _book.BookId
            //            select new
            //            {
            //                ParentName = parent.ParentName,
            //                ChildName = child.ChildName
            //            };

            //foreach (var result in query)
            //{
            //    Console.WriteLine($"Parent: {result.ParentName}, Child: {result.ChildName}");
            //}

            // Update properties
            //parent.ParentName = "Updated Parent Name";
            //child.ChildName = "Updated Child Name";

            //// Save changes
            //context.SaveChanges();
            var cart = await _dbContext.Carts.Include(b => b.Mappings).FirstOrDefaultAsync( c => c.CartId == Id);
            if (cart != null)
            {
                //getting refrenced books collection from cart mapping
                var book = _dbContext.BooksCollection.Where(b => cart.Mappings.Select(m => m.BookId).Contains(b.BookId));

                //reducing book quantity to one                
                book.ToList().ForEach(b => b.Quantity = (b.Quantity --));

                _dbContext.Carts.Remove(cart);

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
