using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class Address
    {
        [Display(Name ="Adres No")]
        public int AddressId { get; set; }
        [Display(Name = "Adres Başlığı")]
        public string AddressTitle { get; set; }
        [Display(Name = "Adres 1")]
        public string Address1 { get; set; }
        [Display(Name = "Adres 2 ")]
        public string Address2 { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
        [Display(Name = "Müşteri No")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
