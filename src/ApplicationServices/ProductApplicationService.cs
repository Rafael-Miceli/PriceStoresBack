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
            if (await FindByName(product.Name) != null)
                throw new Exception("Um produto com este nome já existe");

            await _productContext.AddProduct(product);
        }

        public async Task<ProductDto> FindByName(string productName)
        {
            return await _productContext.FindByName(productName);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            Console.WriteLine("Buscando todos os produtos");
            var products = await _productContext.GetAllWithHistory();

            //Calcular preços min e max de cada produto
            
            return products;
        }
    }
}