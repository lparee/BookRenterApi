using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Carts.Web.Controllers;


using Carts.Core.Models;
using Carts.Service;
using Microsoft.Extensions.Caching.Memory;
using Carts.Core.Entities;


public class CartControllerTest
{
    [Fact]
    public async Task GetBookByName_ReturnsOk_WithValidName()
    {
        // Arrange
        IEnumerable<BooksModel> expectedBook = new List<BooksModel>() { new BooksModel() { BookId = 1, BookName = "Hamlet" } };
        var mockCartService = new Mock<ICartService>();
        mockCartService.Setup(service => service.GetBookByNameAndAuthor(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(expectedBook));

        var mockMemoryCache = new Mock<IMemoryCache>();

        var controller = new CartController(mockCartService.Object, mockMemoryCache.Object);

        // Act
        var actionResult = await controller.GetBookByName("Hamlet");

        // Assert
        var actualBook = Assert.IsType<BooksModel>(actionResult.Value);
        Assert.Equal(expectedBook.First().BookId, actualBook.BookId);
        Assert.Equal(expectedBook.First().BookName, actualBook.BookName);
    }

    [Fact]
    public async Task GetCartByUserId_ReturnsOk_WithValidUserId()
    {
        // Arrange
        int userId = 1;
        var expectedCartData = new CartModel { UserId = userId, /* other properties */ };

        var mockCartService = new Mock<ICartService>();
        var mockMemoryCache = new Mock<IMemoryCache>();

        var controller = new CartController(mockCartService.Object, mockMemoryCache.Object);

        // Act
        var actionResult = await controller.GetCartByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualCartData = Assert.IsType<CartModel>(okResult.Value);
        Assert.Equal(expectedCartData.UserId, actualCartData.UserId);
        // Add additional assertions for other properties if needed
    }

    [Fact]
    public async Task AddToCart_Test()
    {
        // Arrange
        int bookId = 1;
        int userId = 1;

        var emptyCart = new CartModel { UserId = userId, Books = new List<int>() };
        var addedCart = new CartModel { UserId = userId, CartId = 1, Books = new List<int> { bookId } };

        var mockCartService = new Mock<ICartService>();
        mockCartService.Setup(service => service.GetCartByUserId(userId))
                       .ReturnsAsync(emptyCart);
        mockCartService.Setup(service => service.AddToCart(bookId, userId))
                       .ReturnsAsync(addedCart);

        var mockCache = new Mock<IMemoryCache>();
        mockCache.Setup(cache => cache.Set(userId, addedCart));

        var controller = new CartController(mockCartService.Object, mockCache.Object);

        // Act
        var actionResult = await controller.AddToCart(bookId);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        Assert.Equal(nameof(CartController.AddToCart), createdAtActionResult.ActionName);
        // Add additional assertions if needed
    }

    [Fact]
    public async Task CheckOut_ReturnsOk_WhenCartIsNotEmptyAndBooksAreAvailable()
    {
        // Arrange
        int userId = 1;
        int cartId = 1;
        var cart = new CartModel { CartId = cartId, UserId = userId, Books = new List<int> { 1, 2 } };

        var mockCache = new Mock<IMemoryCache>();

        var mockCartService = new Mock<ICartService>();
        

        var controller = new CartController(mockCartService.Object, mockCache.Object);

        // Act
        var actionResult = await controller.CheckOut();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.True(Convert.ToBoolean(okResult.Value));
    }
}
