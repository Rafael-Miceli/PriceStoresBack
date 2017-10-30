using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ProductCategory
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public ProductCategory(string name)
        {            
            Id = Guid.NewGuid().ToString();
            Name = name;
        }
    }        
}