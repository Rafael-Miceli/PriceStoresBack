using System;
using System.Collections.Generic;

namespace Api.Data.Model
{
    public class ProductHistory
    {
        public string Id { get; private set; }
        public ICollection<ProductOfThePast> ProductsOfThePast { get; private set; }        

        public ProductHistory(Api.Models.Product product)
        {
            Id = product.Id;
            ProductsOfThePast = new List<ProductOfThePast>
            {
                new ProductOfThePast(product)
            };
        }
    }

    public class ProductOfThePast
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public float ActualPrice { get; private set; }
        public DateTime DateTimeProductChanged { get; private set; }

        public ProductOfThePast(Api.Models.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            ActualPrice = product.ActualPrice;
            DateTimeProductChanged = DateTime.UtcNow;
        }
    }

}