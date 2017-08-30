using Models;

namespace Api.ApplicationServices.Interfaces
{
    public interface IProductApplicationService
    {
        void AddProduct(Product product);
        Product FindByName(string productName);
    }
}