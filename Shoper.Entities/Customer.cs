using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class Customer
    {
        [Display(Name ="Müşteri No")]
        public int CustomerId { get; set; }
        [Display(Name = "Müşteri Adı")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Müşteri Soyadı")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Eposta")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name ="Telefon")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public int? UserId { get; set; }

        public ICollection<Address> Addresses { get; set; }

    }
}
