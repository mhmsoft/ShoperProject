using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shoper.Entities
{
    public class Coupon
    {
        [Display(Name = "Kupon No")]
        public int couponId { get; set; }
        public int customerId { get; set; }
        [Display(Name = "Kupon Kodu")]
        public string CouponCode { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime created { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime expired { get; set; }
        [Display(Name = "Tutar")]
        public decimal CouponPrice { get; set; }
        [Display(Name = "Geçerlilik durumu")]
        public bool isValid { get; set; }
        public string category { get; set; }
        public Customer Customer { get; set; }
    }
}
