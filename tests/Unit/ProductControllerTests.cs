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

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Product_With_Empty_Name_When_Creating_Product_Then_Return_BadRequest()
        {            
            var productDataMock = new Mock<IProductContext>();
            //productDataMock.Setup(x => x.GetAllWithHistory()).ReturnsAsync(DummiesProducts);
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Post(new ProductVm{Name = "", Price = 10});   

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
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

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Creating_Product_Then_Return_Created()
        {            
            var productDataMock = new Mock<IProductContext>();
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Post(new ProductVm{Name = "Teste", Price = 10}) as ObjectResult;   

            Assert.AreEqual(201, result.StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Creating_Product_Then_Call_Add_ProductRepo()
        {            
            var productDataMock = new Mock<IProductContext>();
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Post(new ProductVm{Name = "Teste", Price = 10});   

            productDataMock.Verify(x => x.AddProduct(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Creating_Product_Then_Call_Add_ProductHistoryRepo()
        {            
            var productDataMock = new Mock<IProductContext>();
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Post(new ProductVm{Name = "Teste", Price = 10});   

            productDataMock.Verify(x => x.AddProductHistory(It.IsAny<ProductHistory>()), Times.Once);
        }


        [TestMethod]
        public async Task Given_A_Valid_Product_When_Updating_ItsPrice_Then_Return_Ok()
        {            
            var productDummie = new Product("Teste", 2);
            var productDataMock = new Mock<IProductContext>();
            productDataMock.Setup(x => x.FindByName("Teste")).ReturnsAsync(productDummie);
            productDataMock.Setup(x => x.GetHistory(productDummie.Id)).ReturnsAsync(new ProductHistory(productDummie));
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Put(new ProductUpdateVm{OldName = "Teste", NewName = "Teste new", Price = 10});   

            Assert.AreEqual(200, (result as StatusCodeResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Updating_ItsPrice_Then_Call_UpdateProductDao()
        {            
            var productDummie = new Product("Teste", 2);
            var productDataMock = new Mock<IProductContext>();
            productDataMock.Setup(x => x.FindByName("Teste")).ReturnsAsync(productDummie);
            productDataMock.Setup(x => x.GetHistory(productDummie.Id)).ReturnsAsync(new ProductHistory(productDummie));
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Put(new ProductUpdateVm{OldName = "Teste", NewName = "Teste new", Price = 10});   

            productDataMock.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Updating_ItsPrice_Then_Call_UpdateProductHistoryDao()
        {            
            var productDummie = new Product("Teste", 2);
            var productDataMock = new Mock<IProductContext>();
            productDataMock.Setup(x => x.FindByName("Teste")).ReturnsAsync(productDummie);
            productDataMock.Setup(x => x.GetHistory(productDummie.Id)).ReturnsAsync(new ProductHistory(productDummie));
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Put(new ProductUpdateVm{OldName = "Teste", NewName = "Teste new", Price = 10});   

            productDataMock.Verify(x => x.UpdateProductHistory(It.IsAny<ProductHistory>()), Times.Once);
        }

        [TestMethod]
        public async Task Given_An_Inexistent_Product_When_Updating_It_Then_Return_BadRequest()
        {            
            var productDataMock = new Mock<IProductContext>();
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Put(new ProductUpdateVm{OldName = "Teste", NewName = "Teste new", Price = 10});   

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Product_When_Updating_It_To_A_Name_Already_Existent_Then_Do_Not_CallUpdate()
        {            
            var productDummie = new Product("Teste new", 3);
            var productDataMock = new Mock<IProductContext>();
            productDataMock.Setup(x => x.FindByName("Teste new")).ReturnsAsync(productDummie);
            productDataMock.Setup(x => x.GetHistory(productDummie.Id)).ReturnsAsync(new ProductHistory(productDummie));
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Put(new ProductUpdateVm{OldName = "Teste", NewName = "Teste new", Price = 10});   

            productDataMock.Verify(x => x.GetHistory(It.IsAny<string>()), Times.Never());
            productDataMock.Verify(x => x.Update(It.IsAny<Product>()), Times.Never());
        }

        [TestMethod]
        public async Task Given_A_Product_When_Updating_It_To_A_Name_Already_Existent_Then_Return_BadRequest()
        {            
            var productDummie = new Product("Teste new", 3);
            var productDataMock = new Mock<IProductContext>();
            productDataMock.Setup(x => x.FindByName("Teste new")).ReturnsAsync(productDummie);
            productDataMock.Setup(x => x.GetHistory(productDummie.Id)).ReturnsAsync(new ProductHistory(productDummie));
            var productService = new ProductApplicationService(productDataMock.Object);
            var sut = new ProductController(productService);

            var result = await sut.Put(new ProductUpdateVm{OldName = "Teste", NewName = "Teste new", Price = 10});   

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
    }    
}
