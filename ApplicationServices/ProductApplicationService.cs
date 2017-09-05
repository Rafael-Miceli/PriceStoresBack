using System;
using Api.ApplicationServices.Interfaces;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.ApplicationServices
{
    public class ProductApplicationService : IProductApplicationService
    {

        private static IMongoDatabase _mongoDb = null;
        //private static string COLLECTION_NAME = "products";                

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
            InitializeMongoDatabase(); 
            //BsonDocument doc = new BsonDocument(product.ToBson());

            var collection = _mongoDb.GetCollection<Product>("products");
            collection.InsertOne(product);
        }

        public Product FindByName(string productName)
        {
            Console.WriteLine($"Buscando produto {productName}");
            InitializeMongoDatabase();
            var collection = _mongoDb.GetCollection<BsonDocument>("products");
            Console.WriteLine(collection.ToJson());
            //AddProduct(new Product("Teste", 0));

            return new Product(productName, 0);
        }
    }
}