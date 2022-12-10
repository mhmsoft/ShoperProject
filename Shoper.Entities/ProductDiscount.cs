using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class ProductDiscount
    {
        [Display(Name ="İndirim No")]
        public int ProductDiscountId { get; set; }
        [Display(Name = "İndirim Adı")]
        public string DiscountName { get; set; }
        [Display(Name = "Ürün No")]
        public int ProductId { get; set; }
        [Display(Name = "İndirim Oran(%)")]
        public int? DiscountRate { get; set; }
        [Display(Name = "İndirim Kupon Kodu")]
        public string? DiscountCode { get; set; }
        [Display(Name = "İndirim Durum")]
        public bool? Status { get; set; }
        [Display(Name = "İndirim Başlangıç Tarihi")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "İndirim Bitiş Tarihi")]
        public DateTime? EndDate { get; set; }
        public Product Product { get; set; }
    }
}
