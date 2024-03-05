using Carts.Service;
using Carts.Core.Models;
using Carts.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Carts.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/carts")]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;
        private readonly int userId = 1; // I am assuming the guset user with userid 1 as Identity is not implemented 
        private readonly int maximumBooksCount = 5;

        /// <summary>
        /// /
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet("{Name}")]
        [ProducesResponseType(typeof(BooksModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BooksModel>> GetBookByName(string Name)
        {
            var result = await _cartService.GetBookByNameAndAuthor(Name, string.Empty);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Author"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BooksModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BooksModel>> GetBookByAuthor(string Author)
        {
            var result = await _cartService.GetBookByNameAndAuthor(string.Empty, Author);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartModel>> AddToCart(CartModel cart)
        {
            var cartBookCount = await _cartService.GetCartByUserId(userId);

            //5 books restriction in cart
            if (cartBookCount.Books.Count() == maximumBooksCount || (cartBookCount.Books.Count() + cart.Books.Count) > 5)
                return BadRequest("you can add up to 5 books in a cart at a time");

            var addedCart = await _cartService.AddToCart(cart, userId);
            
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
            var removed = await _cartService.CheckOut(cartId);
            return Ok(removed);
        }
    }
}
