using Carts.Core.Models;
using Carts.Service;
using Carts.Web.Controllers;
using NSubstitute;

namespace BookRenterApiTest
{
    public class BookTest
    {
        ICartService cartservice = Substitute.For<ICartService>();

        [Fact]
        public void GetBookByName()
        {
            CartController cartCon = new CartController(cartservice);
            //arrange
            cartservice.GetBookByNameAndAuthor("","").Returns(new List<BooksModel>());
            //act
            var response = cartCon.GetBookByName("Abc");
            //assert
            Assert.True(response.IsCompleted);
        }

        [Fact]
        public void GetBookByAuthor()
        {
            CartController cartCon = new CartController(cartservice);
            //arrange
            cartservice.GetBookByNameAndAuthor("", "").Returns(new List<BooksModel>());
            //act
            var response = cartCon.GetBookByAuthor("Mike");
            //assert
            Assert.True(response.IsCompleted);
        }
    }
}