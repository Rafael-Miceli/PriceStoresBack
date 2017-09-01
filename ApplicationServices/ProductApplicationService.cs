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
                var client = new MongoClient();
                _mongoDb = client.GetDatabase("test");
            }
            catch (Exception) { /*Logger.Error(ex, "InitializeMongoDatabase");*/ _mongoDb = null; }
        }

        public void AddProduct(Product product)
        {
            InitializeMongoDatabase(); 
            BsonDocument doc = new BsonDocument();
            doc.Add(new BsonElement("ProductName", product.Name));

            var collection = _mongoDb.GetCollection<BsonDocument>("products");
            collection.InsertOne(doc);
        }

        public Product FindByName(string productName)
        {
            Console.WriteLine($"Buscando produto {productName}");

            return new Product(productName, 0);
        }
    }
}