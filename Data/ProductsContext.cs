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
                var client = new MongoClient("mongodb://192.168.99.100:27017");
                _mongoDb = client.GetDatabase("local");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                _mongoDb = null; 
            }
        }

        public void AddProduct(Product product)
        {
            var productData = new ProductDto(product);
            Product().InsertOne(productData);
            var productHistory = new Api.Data.Model.ProductHistory(product);
            ProductHistory().InsertOne(productHistory);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var dataProducts = Product().Find(_ => true);
            if(!dataProducts.Any())
                return null;                       

            return await dataProducts.ToListAsync();
        }

        public ProductDto FindByName(string name)
        {
            var dataProduct = Product().Find(p => p.Name == name);
            if(!dataProduct.Any())
                return null;           
            
            return dataProduct.ToList().First();    
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
        void AddProduct(Product product);
        Task<IEnumerable<ProductDto>> GetAll();
        ProductDto FindByName(string name);
    }

    
}

