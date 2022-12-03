using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category ProductCategory { get; set; }
        public ICollection<ProductPrice> ProductPrice { get; set; }
        public ICollection<ProductImage> ProductImage { get; set; }
    }

}
