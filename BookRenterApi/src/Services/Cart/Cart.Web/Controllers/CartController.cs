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

        /// <summary>
        /// Get a specific cart by ID.
        /// </summary>
        /// <param name="cartId">The ID of the cart to retrieve.</param>
        /// <returns>The retrieved cart.</returns>
        [HttpGet("{Name}")]
        [ProducesResponseType(typeof(BooksModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BooksModel>> GetBookByName(string Name)
        {
            var result = await _cartService.GetBookByNameAndAuthor(Name, string.Empty);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BooksModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BooksModel>> GetBookByAuthor(string Author)
        {
            var result = await _cartService.GetBookByNameAndAuthor(string.Empty, Author);
            return Ok(result);
        }

        /// <summary>
        /// Add a new item to the user's cart.
        /// </summary>
        /// <param name="cart">The cart item to add.</param>
        /// <param name="LoginId">The AD object ID of the user.</param>
        /// <returns>The added cart item.</returns>
        [HttpPost()]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartModel>> AddToCart(CartModel cart)
        {
            
            var addedCart = await _cartService.AddToCart(cart, userId);
            
            return CreatedAtAction(nameof(AddToCart), new { cartId = addedCart.CartId }, addedCart);
        }

        /// <summary>
        /// Update an existing cart item.
        /// </summary>
        /// <param name="cart">The updated cart item.</param>
        /// <returns>The updated cart item.</returns>
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
        /// Remove an item from the user's cart.
        /// </summary>
        /// <param name="cartId">The ID of the cart item to remove.</param>
        /// <returns>True if the item was successfully removed; otherwise, false.</returns>
        [HttpDelete("{cartId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> CheckOut(int cartId)
        {
            var removed = await _cartService.CheckOut(cartId);
            return Ok(removed);
        }
        private readonly int userId = 1;
    }
}
