using System;
using System.Collections.Generic;

namespace Models
{
    public class Product
    {
        public string Name { get; private set; }
        public float ActualPrice { get; private set; }
        public IEnumerable<ProductHistory> History { get; set; }

        public Product(string name, float actualPrice)
        {
            Name = name;
            ActualPrice = actualPrice;

            CreateNewHistory();
        }

        private void CreateNewHistory()
        {
            History = new List<ProductHistory>{new ProductHistory(this)};
        }
    }

    public class ProductHistory
    {
        public Product Product { get; private set; }
        public DateTime DateTimeProductChanged { get; private set; }

        public ProductHistory(Product product)
        {
            Product = product;
            DateTimeProductChanged = DateTime.UtcNow;
        }
    }
}