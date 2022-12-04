using Shoper.Entities;

namespace Shoper.Management.Models.ViewModels
{
    public class Product_Price_Img_VM
    {
        public Product Product { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductPrice> Prices { get; set; }
    }
}
