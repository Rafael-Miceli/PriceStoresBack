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
    public class ProductHistoryTests
    {
        [TestMethod]
        public void Given_One_Product_With_One_History_When_Calculanting_Min_And_Max_Price_Then_Calculate_It()
        {
            var product = new Product("Teste", 2);
            var sut = new ProductHistory(product);            

            Assert.AreEqual(2, sut.ExpensiverPrice);
            Assert.AreEqual(2, sut.CheaperPrice);
        }

        [TestMethod]
        public void Given_One_Product_With_Two_Histories_When_Calculanting_Min_And_Max_Price_Then_Calculate_It()
        {
            var product1 = new Product("Teste", 2);
            var product2 = new Product("Teste", 3);
            var sut = new ProductHistory(product1);     

            sut.AddToProductsOfThePast(product2);

            Assert.AreEqual(3, sut.ExpensiverPrice);
            Assert.AreEqual(2, sut.CheaperPrice);
        }

        [TestMethod]
        public void Given_One_Product_With_4_Histories_When_Calculanting_Min_And_Max_Price_Then_Calculate_It()
        {
            var product1 = new Product("Teste", 2);
            var product2 = new Product("Teste", 3);
            var product3 = new Product("Teste", 1.3f);
            var product4 = new Product("Teste", 3.75f);
            var sut = new ProductHistory(product1);     

            sut.AddToProductsOfThePast(product2);
            sut.AddToProductsOfThePast(product3);
            sut.AddToProductsOfThePast(product4);

            Assert.AreEqual(3.75f, sut.ExpensiverPrice);
            Assert.AreEqual(1.3f, sut.CheaperPrice);
        }
    }
}
