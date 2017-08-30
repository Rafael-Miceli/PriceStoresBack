using System;
using Api.ApplicationServices.Interfaces;
using Models;

namespace Api.ApplicationServices
{
    public class ProductApplicationService : IProductApplicationService
    {
        public void AddProduct(Product product)
        {
            Console.WriteLine($"Adicionando produto {product.Name}");            
        }

        public Product FindByName(string productName)
        {
            Console.WriteLine($"Buscando produto {productName}");            
            return new Product(productName, 0);
        }
    }
}