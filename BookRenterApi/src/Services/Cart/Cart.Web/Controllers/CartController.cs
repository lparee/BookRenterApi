using Carts.Service;
using Carts.Core.Models;
using Carts.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Carts.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/carts")]
    public class CartController(ICartService cartService, IMemoryCache cache) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;
        private readonly IMemoryCache _cache = cache;

        private readonly int userId = 1; // I am assuming the guset user with userid 1 as Identity is not implemented 
        private readonly int maximumBooksCount = 5;
        private readonly int slidingWindowExpirationInMinutes = 10;

        /// <summary>
        /// /
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        //[HttpGet("{Name}")]
        [HttpGet("BookName", Name = "BookName")]
        [ProducesResponseType(typeof(BooksModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BooksModel>> GetBookByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return BadRequest("Parameter value is expected");

            var result = await _cartService.GetBookByNameAndAuthor(Name, string.Empty);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Author"></param>
        /// <returns></returns>
        [HttpGet("BookAuthor", Name = "BookAuthor")]
        [ProducesResponseType(typeof(BooksModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BooksModel>> GetBookByAuthor(string Author)
        {
            if(string.IsNullOrEmpty(Author))
                return BadRequest("Parameter value is expected");
            var result = await _cartService.GetBookByNameAndAuthor(string.Empty, Author);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookName"></param>
        /// <param name="BookAuthor"></param>
        /// <returns></returns>
        [HttpGet("{BookName}/{BookAuthor}")]
        [ProducesResponseType(typeof(BooksModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BooksModel>> GetBookByAuthorAndName(string BookName, string BookAuthor)
        {
            if(string.IsNullOrEmpty(BookName) && string.IsNullOrEmpty(BookAuthor))
                return BadRequest("Parameters values are expected");

            var result = await _cartService.GetBookByNameAndAuthor(BookName, BookAuthor);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Author"></param>
        /// <returns></returns>
        [HttpGet("UserId", Name = "UserId")]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CartModel>> GetCartByUserId(int userId)
        {
            var cartData = await GetCartFromCache(userId);

            return Ok(cartData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddToCart(int bookId)
        {
            var cartBookCount = await _cartService.GetCartByUserId(userId);

            //5 books restriction in cart
            if (cartBookCount.Books.Count() >= maximumBooksCount)
                return BadRequest("you can add up to 5 books in a cart at a time");

            if (cartBookCount.Books.Contains(bookId))
                return BadRequest("The Book already present in cart");

            var addedCart = await _cartService.AddToCart(bookId, userId);
            _cache.Set(userId, addedCart);

            return CreatedAtAction(nameof(AddToCart), new { cartId = addedCart.CartId }, addedCart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CartModel>> UpdateCart(CartModel cart)
        {

            var updatedCart = await _cartService.UpdateCart(cart);
           
            return Ok(updatedCart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpDelete("{cartId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> CheckOut(int cartId)
        {
            var cart = await GetCartFromCache(userId);

            if (cart == null)
                return BadRequest("cart is empty");

            var checkBooksAvailability = await _cartService.GetBookByIds(cart.Books.ToList(),true);

            if (checkBooksAvailability.Any())
                return BadRequest($"{string.Join(", ", checkBooksAvailability.Select(b => b.BookName))} are not available in library");

            var removed = await _cartService.CheckOut(cartId);
            return Ok(removed);
        }

        private async Task<CartModel?> GetCartFromCache(int userId)
        {
            if (!_cache.TryGetValue(userId, out CartModel? cartData))
            {
                // If not, retrieve the data from the data source
                cartData = await _cartService.GetCartByUserId(userId);

                // Cache the data with a sliding expiration of 5 minutes
                _cache.Set(userId, cartData, TimeSpan.FromMinutes(slidingWindowExpirationInMinutes));
            }

            return cartData;
        }
    }
}
