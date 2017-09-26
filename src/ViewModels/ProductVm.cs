
namespace Api.ViewModels
{
    public class ProductVm
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }    

    public class ProductUpdateVm
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public float Price { get; set; }
    }    
}
