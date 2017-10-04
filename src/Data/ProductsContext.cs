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
        private IMongoCollection<Product> Products {get; set;}
        private IMongoCollection<ProductHistory> ProductsHistory {get; set;}

        public ProductContext()
        {
            InitializeMongoDatabase();
            Products = _mongoDb.GetCollection<Product>("products");
            ProductsHistory = _mongoDb.GetCollection<ProductHistory>("productsHistory");
        }

        private void InitializeMongoDatabase()
        {
            try
            {
                var client = new MongoClient("mongodb://mongo:27017");                
                //var client = new MongoClient("mongodb://192.168.99.100:27017");
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
            await Products.InsertOneAsync(product);            
        }

        public async Task Update(Product product)
        {            
            await Products.ReplaceOneAsync(p => p.Id == product.Id, product);            
        }

        public async Task UpdateProductHistory(ProductHistory productHistory)
        {
            await ProductsHistory.ReplaceOneAsync(h => h.Id == productHistory.Id, productHistory);            
        }

        public async Task AddProductHistory(ProductHistory productHistory)
        {            
            await ProductsHistory.InsertOneAsync(productHistory);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var dataProducts = Products.Find(_ => true);
            if(!dataProducts.Any())
                return null;                       

            return await dataProducts.ToListAsync();
        }

        public async Task<IEnumerable<ProductHistory>> GetAllWithHistory()
        {
            var productWithHistories = await Products
                .Aggregate()
                .Lookup<Product, ProductHistory, ProductWithHistories>(
                    ProductsHistory,
                    r => r.Id,
                    h => h.Id,
                    j => j.ProductsHistory
                ).ToListAsync();      
            
            return productWithHistories.SelectMany(p => p.ProductsHistory);
        }

        public async Task<Product> FindByName(string name)
        {
            var dataProduct = Products.Find(p => p.Name == name);
            if(!dataProduct.Any())
                return null;           
            
            return await dataProduct.FirstOrDefaultAsync();    
        }        

        public async Task<ProductHistory> GetHistory(string id)
        {
            var dataProduct = ProductsHistory.Find(p => p.Id == id);
            if(!dataProduct.Any())
                return null;           
            
            return await dataProduct.FirstOrDefaultAsync();    
        }     
    }

    public interface IProductContext
    {
        Task AddProduct(Product product);
        Task Update(Product product);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> FindByName(string name);
        Task<IEnumerable<ProductHistory>> GetAllWithHistory();
        Task AddProductHistory(ProductHistory productHistory);
        Task<ProductHistory> GetHistory(string id);
        Task UpdateProductHistory(ProductHistory productHistory);
    }    
}

