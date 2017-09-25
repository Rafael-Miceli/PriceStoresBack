using Api.ApplicationServices;
using Api.Data;
using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Api.ViewModels;
using Api.Models;

namespace tests.Unit
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public async Task Given_A_Product_Without_Price_When_Creating_Product_Then_Return_BadRequest()
        {            
            var productDataMock = new Mock<IProductContext>();
            //productDataMock.Setup(x => x.GetAllWithHistory()).ReturnsAsync(DummiesProducts);
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Post(new ProductVm{Name = "Teste", Price = 0});   

            Assert.AreEqual(400, (result as BadRequestResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Product_With_Empty_Name_When_Creating_Product_Then_Return_BadRequest()
        {            
            var productDataMock = new Mock<IProductContext>();
            //productDataMock.Setup(x => x.GetAllWithHistory()).ReturnsAsync(DummiesProducts);
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Post(new ProductVm{Name = "", Price = 10});   

            Assert.AreEqual(400, (result as BadRequestResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Product_Already_Exists_When_Creating_Product_Then_Return_BadRequest()
        {            
            var dummieProduct = new Product("Teste", 2);
            var productDataMock = new Mock<IProductContext>();
            productDataMock.Setup(x => x.FindByName("Teste")).ReturnsAsync(dummieProduct);
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Post(new ProductVm{Name = "Teste", Price = 10});   

            Assert.AreEqual(400, (result as BadRequestResult).StatusCode);
        }
    }
}
