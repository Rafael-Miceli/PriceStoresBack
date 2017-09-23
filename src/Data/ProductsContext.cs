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
        private IMongoCollection<ProductDto> Products {get; set;}
        private IMongoCollection<Api.Data.Model.ProductHistory> ProductsHistory {get; set;}

        public ProductContext()
        {
            InitializeMongoDatabase();
            Products = _mongoDb.GetCollection<ProductDto>("products");
            ProductsHistory = _mongoDb.GetCollection<Api.Data.Model.ProductHistory>("productsHistory");
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
            await Products.InsertOneAsync(productData);
            var productHistory = new Api.Data.Model.ProductHistory(product);
            await ProductsHistory.InsertOneAsync(productHistory);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var dataProducts = Products.Find(_ => true);
            if(!dataProducts.Any())
                return null;                       

            return await dataProducts.ToListAsync();
        }

        public async Task<IEnumerable<ProductWithHistories>> GetAllWithHistory()
        {
            return await Products
                .Aggregate()
                .Lookup<ProductDto, Api.Data.Model.ProductHistory, ProductWithHistories>(
                    ProductsHistory,
                    r => r.Id,
                    h => h.Id,
                    j => j.ProductHistory
                ).ToListAsync();         
        }

        public async Task<ProductDto> FindByName(string name)
        {
            var dataProduct = Products.Find(p => p.Name == name);
            if(!dataProduct.Any())
                return null;           
            
            return await dataProduct.FirstOrDefaultAsync();    
        }        
    }

    public interface IProductContext
    {
        Task AddProduct(Product product);
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> FindByName(string name);
        Task<IEnumerable<ProductWithHistories>> GetAllWithHistory();
    }    
}

