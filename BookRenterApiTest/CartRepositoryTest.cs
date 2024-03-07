using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carts.Data;
using System.Data;
using Carts.Core.Entities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class CartRepositoryTests
{

    private static IQueryable<TEntity> MockDbSetQuriable<TEntity>(List<TEntity> list) where TEntity : class
    {
        var queryable = list.AsQueryable();
        var dbSetMock = new Mock<DbSet<TEntity>>();
        dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        dbSetMock.Setup(d => d.Include(It.IsAny<string>())).Returns(dbSetMock.Object);
        return dbSetMock.Object;
    }

    private static DbSet<T> MockDbSet<T>(List<T> list) where T : class
    {
        var queryable = list.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        return dbSetMock.Object; // Return DbSet<T> instead of Mock<DbSet<T>>
    }

    [Fact]
    public async Task GetCartByUserId_ReturnsCart()
    {
        // Arrange
        var userId = 1;
        var mockDbContext = new Mock<BookRenterDbContext>();
        var cartRepository = new CartRepository(mockDbContext.Object);
        var expectedCartBookMapping = new List<CartBookMapping>() { new CartBookMapping() { CartId = 1, BookId = 1 , CBMappingId = 1} };
        var expectedCart = new Carts.Core.Entities.Cart (){ CartId = 1, UserId = userId, Mappings = expectedCartBookMapping  }; 
        mockDbContext.Setup(db => db.Carts.FindAsync(userId)).ReturnsAsync(expectedCart);

        // Act
        var result = await cartRepository.GetCartByUserId(userId);

        // Assert
        Assert.Equal(expectedCart, result);
    }

    [Fact]
    public async Task AddToCart_AddsCartItem()
    {
        // Arrange
        var bookId = 2;
        var userId = 1;
        var mockDbContext = new Mock<BookRenterDbContext>();
        var cartRepository = new CartRepository(mockDbContext.Object);
        var expectedCartBookMapping = new List<CartBookMapping>() { new CartBookMapping() { CartId = 1, BookId = bookId, CBMappingId = 2 } };
        var expectedCart = new Carts.Core.Entities.Cart() { CartId = 1, UserId = userId, Mappings = expectedCartBookMapping };

        mockDbContext.Setup(db => db.Carts.FindAsync(userId)).ReturnsAsync(expectedCart);

        // Act
        var result = await cartRepository.AddToCart(bookId, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCart, result);
    }

    [Fact]
    public async Task GetBookByNameAndAuthor_ReturnsMatchingBooks()
    {
        // Arrange
        var name = "Book1";
        var author = "Author1";
        var expectedBooks = new List<BookInventory>
        {
            new BookInventory { BookId = 1, BookName = name, Author = author },
            new BookInventory { BookId = 2, BookName = name, Author = author }
        };

        // Mocking the DbContext
        var mockDbContext = new Mock<BookRenterDbContext>();
        var cartRepository = new CartRepository(mockDbContext.Object);
        var mockDbSet = MockDbSet(expectedBooks);
        mockDbContext.Setup(db => db.BooksCollection);
        

        // Act
        var result = await cartRepository.GetBookByNameAndAuthor(name, author);

        // Assert
        Assert.Equal(expectedBooks, result);
    }

    [Fact]
    public async Task GetBookByIds_ReturnsMatchingBooks()
    {
        // Arrange
        var bookIds = new List<int> { 1, 2 };
        var isNotAvailable = true;
        var expectedBooks = new List<BookInventory>
        {
            new BookInventory { BookId = 1, Quantity = 2 },
            new BookInventory { BookId = 2, Quantity = 3 }
        };

        var mockDbContext = new Mock<BookRenterDbContext>();
        var mockDbSet = MockDbSet(expectedBooks);
        mockDbContext.Setup(db => db.BooksCollection)
                     .Returns(mockDbSet);

        var repository = new CartRepository(mockDbContext.Object);

        // Act
        var result = await repository.GetBookByIds(bookIds, isNotAvailable);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(expectedBooks[0], result);
        Assert.Contains(expectedBooks[1], result);
    }

    [Fact]
    public async Task CheckOut_RemovesCartAndMappingsAndUpdateBookQuantities()
    {
        // Arrange
        var cartId = 1;
        var cart = new Carts.Core.Entities.Cart
        {
            CartId = cartId,
            Mappings = new List<CartBookMapping>
            {
                new CartBookMapping { BookId = 1 },
                new CartBookMapping { BookId = 2 }
            }
        };

        var carts = new List<Carts.Core.Entities.Cart> { cart };

        var books = new List<BookInventory>
        {
            new BookInventory { BookId = 1, Quantity = 2 },
            new BookInventory { BookId = 2, Quantity = 3 }
        };

        var mockDbSet = MockDbSet(books);
        var mockDBCart = MockDbSet(carts);
        var mockDbContext = new Mock<BookRenterDbContext>();

        mockDbContext.Setup(db => db.Carts).Returns(MockDbSet(new List<Carts.Core.Entities.Cart> { cart }));
        mockDbContext.Setup(db => db.BooksCollection)
                     .Returns(mockDbSet);

        var repository = new CartRepository(mockDbContext.Object);

        // Act
        var result = await repository.CheckOut(cartId);

        // Assert
        Assert.True(result);
        Assert.Empty(mockDbContext.Object.Carts);
        Assert.Empty(mockDbContext.Object.CartBookMapping);
        Assert.Equal(1, books[0].Quantity);
        Assert.Equal(2, books[1].Quantity);
    }
    // Write similar test cases for other methods (UpdateCart, CheckOut, GetBookByIds, etc.)
}
