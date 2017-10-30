using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Product
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public float ActualPrice { get; private set; }
        public ProductCategory Category { get; set; }

        public Product(string name, float actualPrice)
        {            
            Id = Guid.NewGuid().ToString();
            Name = name;
            ActualPrice = actualPrice;

            //Notify
            //CreateNewHistory();
        }

        public void UpdatePrice(float price)
        {
            ActualPrice = price;
            //Notify
            //History.Add(new ProductHistory(this));            
        }

        public void UpdateName(string name)
        {
            Name = name;
            //Notify
            //History.Add(new ProductHistory(this));            
        }
    }        
}