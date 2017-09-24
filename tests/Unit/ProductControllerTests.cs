using Api.ApplicationServices;
using Api.Data;
using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace tests.Unit
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        [Ignore]
        public async Task When_Listing_Products_Then_Return_Its_Price_And_Min_And_Max_Prices_Also()
        {
            //var DummiesProducts = new List<ProductWithHistories>();
            var productDataMock = new Mock<IProductContext>();
            //productDataMock.Setup(x => x.GetAllWithHistory()).ReturnsAsync(DummiesProducts);
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Get();   

            Console.WriteLine($"Recebido {result.Count()} produtos");

            Assert.IsTrue(result.Count() > 0);
            // Assert.IsTrue(result.First().MaxPrice > 0);
            // Assert.IsTrue(result.First().MinPrice > 0);
        }
    }
}
