using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class Product
    {
        [Display(Name ="Ürün Kodu")]
        public int ProductId { get; set; }
        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }
        [Display(Name = "Ürün Stoğu")]
        public int ProductStock { get; set; }
        [Display(Name = "Ürün Açıklaması")]
        public string Description { get; set; }
        [Display(Name = "Kategori Kodu")]
        public int CategoryId { get; set; }
        [Display(Name = "Marka Kodu")]
        public int ManifactureId { get; set; }
        public   Manifacture Manifacture { get; set; }
        public virtual Category ProductCategory { get; set; }
        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductDiscount> ProductDiscount { get; set; }
        public virtual ICollection<ProductComment> ProductComment { get; set; }
        public virtual ICollection<ProductItemValue> ProductItemValue { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }

}
