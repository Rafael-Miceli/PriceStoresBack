using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Api.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Data
{
    public class ProductContext: IProductContext
    {
        private IMongoDatabase _mongoDb;

        public ProductContext()
        {
            InitializeMongoDatabase();
        }

        private void InitializeMongoDatabase()
        {
            try
            {
                var client = new MongoClient("mongodb://localhost:27017");
                _mongoDb = client.GetDatabase("local");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                _mongoDb = null; 
            }
        }

        public async Task AddProduct(Product product)
        {
            var productData = new ProductDto(product);
            await Product().InsertOneAsync(productData);
            var productHistory = new Api.Data.Model.ProductHistory(product);
            await ProductHistory().InsertOneAsync(productHistory);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var dataProducts = Product().Find(_ => true);
            if(!dataProducts.Any())
                return null;                       

            return await dataProducts.ToListAsync();
        }

        public async Task<ProductDto> FindByName(string name)
        {
            var dataProduct = Product().Find(p => p.Name == name);
            if(!dataProduct.Any())
                return null;           
            
            return await dataProduct.FirstOrDefaultAsync();    
        }

        private IMongoCollection<ProductDto> Product()
        {
            return _mongoDb.GetCollection<ProductDto>("products");
        }
        
        private IMongoCollection<Api.Data.Model.ProductHistory> ProductHistory()
        {
            return _mongoDb.GetCollection<Api.Data.Model.ProductHistory>("productsHistory");
        }
    }

    public interface IProductContext
    {
        Task AddProduct(Product product);
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> FindByName(string name);
    }

    
}

