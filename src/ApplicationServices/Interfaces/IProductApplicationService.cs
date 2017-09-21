using System.Collections.Generic;
using Api.ViewModels;
using Api.Models;
using System.Threading.Tasks;

namespace Api.ApplicationServices.Interfaces
{
    public interface IProductApplicationService
    {
        Task AddProduct(Product product);
        Task<ProductDto> FindByName(string productName);
        Task<IEnumerable<ProductDto>> GetAll();
    }
}