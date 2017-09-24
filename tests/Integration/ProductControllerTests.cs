using Api.ApplicationServices;
using Api.Data;
using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace tests.Integration
{
    [TestClass]
    public class ProductControllerIntegrationTests
    {
        [TestMethod]
        public async Task When_Listing_Products_Then_Return_Its_Price_And_Min_And_Max_Prices_Also()
        {
            var productData = new ProductContext();
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var result = await sut.Get();   

            Console.WriteLine($"Recebido {result.Count()} produtos");

            Assert.IsTrue(result.Count() > 0);
            Assert.IsTrue(result.First().ExpensiverPrice > 0);
            Assert.IsTrue(result.First().CheaperPrice > 0);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Creating_Product_Then_Create_Product_With_History()
        {
            var productData = new ProductContext();
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var productVm = new ProductVm {
                Name = "Teste",
                LastPrice = 2
            };

            var result = await sut.Post(productVm);   

            Assert.AreEqual(201, (result as CreatedResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Creating_Product_Then_Create_Product_With_History_And_Cheaper_And_Expensiver_Prices_Calculated()
        {
            var productData = new ProductContext();
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var productVm = new ProductVm {
                Name = "Teste 2",
                LastPrice = 3
            };

            var result = await sut.Post(productVm);   

            Assert.AreEqual(201, (result as CreatedResult).StatusCode);
        }
    }
}
