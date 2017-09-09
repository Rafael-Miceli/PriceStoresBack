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
            InitializeMongoDatabase(); 
            BsonDocument doc = new BsonDocument{{"Name", product.Name}};

            //doc.Add(new BsonElement("ProductName", product.Name));

            try
            {
                var products = _mongoDb.GetCollection<BsonDocument>("products");
                products.InsertOne(doc);    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }             
        }

        public Product FindByName(string productName)
        {
            InitializeMongoDatabase(); 
            //Console.WriteLine($"Buscando produto {productName}");
            var products = _mongoDb.GetCollection<BsonDocument>("products");
            Console.WriteLine("Total de Produtos " + products.AsQueryable().First());
            //InitializeMongoDatabase();
            //AddProduct(new Product("Teste", 0));

            return new Product(productName, 0);
        }
    }
}