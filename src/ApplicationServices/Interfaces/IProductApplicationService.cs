using System.Collections.Generic;
using Api.ViewModels;
using Api.Models;
using System.Threading.Tasks;

namespace Api.ApplicationServices.Interfaces
{
    public interface IProductApplicationService
    {
        Task AddProduct(Product product);
        Task<Product> FindByName(string productName);
        Task<IEnumerable<ProductHistory>> GetAll();
    }
}