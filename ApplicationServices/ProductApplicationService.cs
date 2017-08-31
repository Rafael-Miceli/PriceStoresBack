using System;
using Api.ApplicationServices.Interfaces;
using Models;
using MongoDB.Driver;

namespace Api.ApplicationServices
{
    public class ProductApplicationService : IProductApplicationService
    {

        private static IMongoDatabase MONGO_DATABASE = null;
        //private static string COLLECTION_NAME = "products";                

        private void InitializeMongoDatabase()
        {
            try
            {
                var client = new MongoClient("");
                MONGO_DATABASE = client.GetDatabase("");
            }
            catch (Exception ex) { /*Logger.Error(ex, "InitializeMongoDatabase");*/ MONGO_DATABASE = null; }
        }

        public void AddProduct(Product product)
        {
            Console.WriteLine($"Adicionando produto {product.Name}");    

            var client = new MongoClient();
        }

        public Product FindByName(string productName)
        {
            Console.WriteLine($"Buscando produto {productName}");

            return new Product(productName, 0);
        }
    }
}