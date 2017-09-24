using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ProductHistory
    {
        public string Id { get; set; }
        public List<ProductOfThePast> ProductsOfThePast { get; private set; }   
        public float CheaperPrice { get; private set; }     
        public float ExpensiverPrice { get; private set; }     

        public ProductHistory(Product product)
        {            
            Id = product.Id;
            ProductsOfThePast = new List<ProductOfThePast>();  
            AddToProductsOfThePast(product);                              
        }

        public ProductHistory(Product product, List<ProductOfThePast> productsOfThePast)
        {            
            Id = product.Id;
            ProductsOfThePast = productsOfThePast;

            if(ProductsOfThePast == null)
                ProductsOfThePast = new List<ProductOfThePast>();  

            AddToProductsOfThePast(product);                              
        }

        private void AddToProductsOfThePast(Product product)
        {            
            ProductsOfThePast.Add(new ProductOfThePast(product));      
        }
    }

    public class ProductOfThePast
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public DateTime DateTimeProductChanged { get; private set; }

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


        // public async Task CalculateMinAndMax()
        // {
        //     await Task.Run(() => {
        //         MinPrice = ProductsHistory.First().ProductsOfThePast.Min(p => p.ActualPrice);
        //         MaxPrice = ProductsHistory.First().ProductsOfThePast.Max(p => p.ActualPrice);
        //     });
        // }
    }
}