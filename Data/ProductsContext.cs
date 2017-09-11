using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Api.Models;
using System.Linq;
using System.Collections.Generic;

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

        public void AddProduct(Product product)
        {
            var productData = new Api.Data.Model.Product(product);
            Product().InsertOne(productData);
            var productHistory = new Api.Data.Model.ProductHistory(product);
            ProductHistory().InsertOne(productHistory);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var dataProducts = Product().Find(p => p.Id != null).ToList();
            var products = MapDataProductToModelProduct(dataProducts);

            return products;
        }

        private IEnumerable<ProductDto> MapDataProductToModelProduct(List<Api.Data.Model.Product> dataProducts)
        {
            return dataProducts.Select(d => new ProductDto(d.Id, d.Name, d.ActualPrice));
        }

        private IMongoCollection<Api.Data.Model.Product> Product()
        {
            return _mongoDb.GetCollection<Api.Data.Model.Product>("products");
        }
        private IMongoCollection<Api.Data.Model.ProductHistory> ProductHistory()
        {
            return _mongoDb.GetCollection<Api.Data.Model.ProductHistory>("productsHistory");
        }
    }

    public interface IProductContext
    {
        void AddProduct(Product product);
        IEnumerable<ProductDto> GetAll();
    }

    
}

