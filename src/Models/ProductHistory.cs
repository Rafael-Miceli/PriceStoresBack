using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class ProductHistory
    {
        [BsonElement("_id")]
        public string ProductId { get; private set; }
        public List<ProductOfThePast> ProductsOfThePast { get; private set; }   
        public float CheaperPrice { get; private set; }     
        public float ExpensiverPrice { get; private set; }
        public ProductCategory Category { get; set; }

        public ProductHistory(Product product)
        {            
            ProductId = product.Id;
            ProductsOfThePast = new List<ProductOfThePast>();              
            AddToProductsOfThePast(product);                               
        }

        public void AddToProductsOfThePast(Product product)
        {            
            ProductsOfThePast.Add(new ProductOfThePast(product));      
            CalculateMinAndMax();           
        }

        private void CalculateMinAndMax()
        {
            CheaperPrice = ProductsOfThePast.Min(p => p.Price);
            ExpensiverPrice = ProductsOfThePast.Max(p => p.Price);
        }
    }

    public class ProductOfThePast
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public DateTime DateTimeProductChanged { get; private set; }
        public ProductCategory Category { get; set; }

        public ProductOfThePast(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.ActualPrice;
            DateTimeProductChanged = DateTime.UtcNow;
        }
    }

    public class ProductWithHistories: ProductHistory
    {
        public string Name { get; private set; }
        public float ActualPrice { get; private set; }

        public ProductWithHistories(Product product) : base(product)
        {
        }

        //Apesar de ser uma lista de historicos vai haver apenas um historico com match de produto.
        //O motivo de ser uma lista é por convenção do Mongo para o Agreggate ($Join)
        public IEnumerable<ProductHistory> ProductsHistory { get; set; }
    }
}