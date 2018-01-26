using System.Threading.Tasks;
using Api.Models;

namespace Api.ApplicationServices.Interfaces 
{
    public interface IWantToNotify
    {
        Task NewProduct(Product message);
        Task UpdateProduct(Product message);
    }
}