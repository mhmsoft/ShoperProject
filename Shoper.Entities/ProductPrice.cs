using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shoper.Entities
{
    public class ProductPrice
    {
        [Display(Name = "Fiyat No")]
        public int PriceId { get; set; }
        [Display(Name = "Fiyat birimi")]
        public UnitPrice UnitPrice { get; set; }
        [Display(Name = "Ürün Kodu")]
        public int ProductId { get; set; }  // foreign key
        [Display(Name = "Ürün Fiyatı")]
        public decimal Price { get; set; }
        [Display(Name = "Geçerli Fiyat")]
        public bool isValidFlag { get; set; }

        public Product Product { get; set; }
    }
    public enum UnitPrice
    {
        [Display(Name ="₺")]
        Lira,
        [Display(Name = "$")]
        Dolar,
        [Display(Name = "€")]
        Euro,
        [Display(Name = "£")]
        Pound

    }
}
