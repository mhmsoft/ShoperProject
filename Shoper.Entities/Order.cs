using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class Order
    {
        [Display(Name ="Sipariş No")]
        public int OrderId { get; set; }
        [Display(Name = "Sipariş Tarihi")]
        public DateTime? OrderDate { get; set; }

        public int? CustomerId { get; set; }
        [Display(Name = "Müşteri")]
        public string fullName { get; set; }
        [Display(Name = "Adres")]
        public string? Adres1 { get; set; }
        [Display(Name = "Adres2")]
        public string Adres2 { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
        [Display(Name = "Gsm")]
        public string Phone { get; set; }
        [Display(Name = "Eposta")]
        public string mail { get; set; }
        [Display(Name = "Sipariş Durum")]
        public OrderStatus Status { get; set; }
        [Display(Name = "Miktar")]
        public float TotalAmount { get; set; }
        [Display(Name = "Onay")]
        public bool isVerified { get; set; }
        [Display(Name = "Aktif Mi")]
        public bool isActive { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
    public enum OrderStatus
    {
        [Display(Name = "Hazırlanıyor")]
        Preparing,
        [Display(Name = "Kargoda")]
        Ontheway,
        [Display(Name = "Teslim Edildi")]
        Shipped,
        [Display(Name = "İptal Edildi")]
        Cancelled
    }
}
