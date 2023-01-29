using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class WishList
    {
        [Display(Name ="Favori No")]
        public int wishListId { get; set; }
        public int customerId { get; set; }
        [Display(Name = "Eklenme Tarihi")]
        public DateTime? added { get; set; }
        public int productId { get; set; }
        public Product product { get; set; }
        public Customer Customer { get; set; }

    }
}
