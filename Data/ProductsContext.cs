using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Api.Models;

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

        public IMongoCollection<Product> Product()
        {
            return _mongoDb.GetCollection<Product>("products");
        }
    }

    public interface IProductContext
    {
        IMongoCollection<Product> Product();
    }

    
}

