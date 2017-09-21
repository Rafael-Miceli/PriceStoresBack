using Api.ApplicationServices;
using Api.Data;
using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace tests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void When_Listing_Products_Then_Return_Its_Price_And_Min_And_Max_Prices_Also()
        {
            var productDataMock = new Mock<IProductContext>();
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = sut.Get().Result;   

            Assert.IsTrue(result.First().MaxPrice > 0);
            Assert.IsTrue(result.First().MinPrice > 0);
        }
    }
}
