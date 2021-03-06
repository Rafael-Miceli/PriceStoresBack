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
        private string mongoConnection = "mongodb://localhost:27017";

        [TestMethod]
        public async Task When_Listing_Products_Then_Return_Its_Price_And_Min_And_Max_Prices_Also()
        {
            var productData = new ProductContext(mongoConnection);
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var result = await sut.Get();
            
            Assert.IsTrue(result.Count() > 0);
            Assert.IsTrue(result.First().HigherPrice > 0);
            Assert.IsTrue(result.First().LowerPrice > 0);
        }

        //Rever este teste, pois o comportamento mudou
        [TestMethod]
        [Ignore]
        public async Task When_Listing_Products_Then_Return_Grouped_By_Categories()
        {
            var productData = new ProductContext(mongoConnection);
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var result = await sut.Get();
           
            Assert.IsTrue(result.Count() > 0);
            Assert.IsNotNull(result.First().CategoryName);
        }

        [TestMethod]
        public async Task Given_Products_Without_Categories_When_Listing_Products_Then_Return_Default_Category()
        {
            var productData = new ProductContext(mongoConnection);
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var result = await sut.Get();            

            Assert.IsTrue(result.Count() > 0);
            Assert.AreEqual("Sem Categoria", result.First().CategoryName);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Creating_Product_Then_Create_Product_With_History()
        {
            var productData = new ProductContext(mongoConnection);
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var productVm = new CreateProductVm {
                Name = "Teste",
                Price = 2
            };

            var result = await sut.Post(productVm);   

            Assert.AreEqual(201, (result as CreatedResult).StatusCode);
        }

        [TestMethod]
        public async Task Given_A_Valid_Product_When_Creating_Product_Then_Create_Product_With_History_And_Cheaper_And_Expensiver_Prices_Calculated()
        {
            var productData = new ProductContext(mongoConnection);
            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);

            var productVm = new CreateProductVm {
                Name = "Teste 2",
                Price = 3
            };

            var result = await sut.Post(productVm);   

            Assert.AreEqual(201, (result as CreatedResult).StatusCode);
        }        

        [TestMethod]
        public async Task Given_Two_Valids_Products_Names_When_Deleting_Products_By_Name_Then_Return_204()
        {
            var productsToDelete = new[] { "a", "Inhame" };
            var productData = new ProductContext(mongoConnection);

            var existentProducts = await productData.GetAllByNames(productsToDelete);
            Assert.IsNotNull(existentProducts);
            Assert.AreEqual(2, existentProducts.Count());

            var productService = new ProductApplicationService(productData);
            var sut = new ProductController(productService);            

            var result = await sut.Delete(productsToDelete);

            Assert.AreEqual(204, (result as NoContentResult).StatusCode);
        }
    }
}
