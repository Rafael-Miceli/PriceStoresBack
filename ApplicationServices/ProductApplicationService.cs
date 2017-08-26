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
    }
}