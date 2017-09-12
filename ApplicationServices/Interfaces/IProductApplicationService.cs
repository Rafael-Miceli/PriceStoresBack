using System.Collections.Generic;
using Api.ViewModels;
using Api.Models;
using System.Threading.Tasks;

namespace Api.ApplicationServices.Interfaces
{
    public interface IProductApplicationService
    {
        void AddProduct(Product product);
        ProductDto FindByName(string productName);
        Task<IEnumerable<ProductDto>> GetAll();
    }
}