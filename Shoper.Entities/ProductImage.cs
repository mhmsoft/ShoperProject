using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shoper.Entities
{
    public class ProductImage
    {
        [Display(Name = "Resim No")]
        public int ImageId { get; set; }
        [Display(Name = "Ürün Kodu")]
        public int ProductId { get; set; } // foreign key
        [Display(Name = "Ürün Resim")]
        public byte[] Image{ get; set; } // byte şeklinde yazacak

        public Product Product { get; set; }
    }
}
