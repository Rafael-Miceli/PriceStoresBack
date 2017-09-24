using Api.ApplicationServices;
using Api.Data;
using Api.Models;
using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace tests.Unit
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public async Task Given_One_Product_With_One_History_When_Calculanting_Min_And_Max_Price_Then_Calculate_It()
        {
            // var product = new ProductDto("Test", 2);
            // // product.ProductsHistory = new List<Api.Data.Model.ProductHistory>{
            // //     new Api.Data.Model.ProductHistory()
            // // }
            // var sut = new ProductWithHistories(product);

            // var result = await sut.Get();   

            // Console.WriteLine($"Recebido {result.Count()} produtos");

            // Assert.IsTrue(result.Count() > 0);
            // Assert.IsTrue(result.First().MaxPrice > 0);
            // Assert.IsTrue(result.First().MinPrice > 0);
        }
    }
}
