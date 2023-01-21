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
        public DateTime? OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public string fullName { get; set; }
        public string? Adres1 { get; set; }
        public string Adres2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string mail { get; set; }
        public OrderStatus Status { get; set; }
        public float TotalAmount { get; set; }
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
