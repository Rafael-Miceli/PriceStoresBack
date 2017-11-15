
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class CreateProductVm
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }    

    public class UpdateProductVm
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public float Price { get; set; }
    }
    
    public class ProductResumeVm
    {
        public string Name { get; set; }
        public float LastPrice { get; set; }
        public float LowerPrice { get; set; }
        public float HigherPrice { get; set; }
        public string CategoryName { get; set; }
    }    
}
