using Api.Models;

namespace Api.ApplicationServices.Interfaces 
{
    public interface IWantToNotify
    {
        void NewProduct(Product message);
        void UpdateProduct(Product message);
    }
}