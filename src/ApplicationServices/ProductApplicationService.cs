using System;
using System.Collections.Generic;
using Api.ApplicationServices.Interfaces;
using Api.ViewModels;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Api.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ApplicationServices
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductContext _productContext;

        public ProductApplicationService(IProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task AddProduct(Product product)
        {
            if(product.ActualPrice <= 0)
                throw new Exception("Produto não pode ter preço Zero ou negativo.");

            if(string.IsNullOrEmpty(product.Name))
                throw new Exception("Produto não pode ter nome vazio.");

            if (await FindByName(product.Name) != null)
                throw new Exception("Um produto com este nome já existe");

            await _productContext.AddProduct(product);

            var productHistory = new ProductHistory(product);
            await _productContext.AddProductHistory(productHistory);
        }

        public async Task<Product> FindByName(string productName)
        {
            return await _productContext.FindByName(productName);
        }

        public async Task<IEnumerable<ProductHistory>> GetAll()
        {
            Console.WriteLine("Buscando todos os produtos");
            var products = await _productContext.GetAllWithHistory();

            return products;
        }
    }
}