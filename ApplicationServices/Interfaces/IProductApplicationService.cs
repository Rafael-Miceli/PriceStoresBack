using System.Collections.Generic;
using Api.ViewModels;
using Api.Models;

namespace Api.ApplicationServices.Interfaces
{
    public interface IProductApplicationService
    {
        void AddProduct(Product product);
        ProductDto FindByName(string productName);
        IEnumerable<ProductDto> GetAll();
    }
}