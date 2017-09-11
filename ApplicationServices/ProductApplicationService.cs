using System;
using System.Collections.Generic;
using Api.ApplicationServices.Interfaces;
using Api.ViewModels;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Api.Data;
using System.Linq;

namespace Api.ApplicationServices
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductContext _productContext;

        public ProductApplicationService(IProductContext productContext)
        {
            _productContext = productContext;
        }

        public void AddProduct(Product product)
        {
            _productContext.AddProduct(product);
        }

        public Product FindByName(string productName)
        {
            Console.WriteLine($"Buscando produto {productName}");
            return new Product(productName, 0);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            Console.WriteLine("Buscando todos os produtos");
            var products = _productContext.GetAll();
            Console.WriteLine("produto encontrado " + products.First().Name);
            return products;
        }
    }
}