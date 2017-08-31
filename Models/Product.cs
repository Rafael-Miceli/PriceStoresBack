using System;
using System.Collections.Generic;

namespace Models
{
    public class Product
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public float ActualPrice { get; private set; }
        public ICollection<ProductHistory> History { get; set; }

        public Product(string name, float actualPrice)
        {
            Name = name;
            ActualPrice = actualPrice;

            //Notify
            CreateNewHistory();
        }

        private void CreateNewHistory()
        {
            History = new List<ProductHistory>{new ProductHistory(this)};
        }

        public void UpdatePrice(float price)
        {
            ActualPrice = price;
            //Notify
            History.Add(new ProductHistory(this));            
        }
    }

    public class ProductHistory
    {
        public ProductOfThePast Product { get; private set; }
        public DateTime DateTimeProductChanged { get; private set; }

        public ProductHistory(Product product)
        {
            Product = new ProductOfThePast(product);
            DateTimeProductChanged = DateTime.UtcNow;

            //Notify
        }
    }

    public class ProductOfThePast
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }

        public ProductOfThePast(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.ActualPrice;
        }
    }
}