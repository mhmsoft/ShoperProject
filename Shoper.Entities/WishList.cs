using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class WishList
    {
        public int wishListId { get; set; }
        public int userId { get; set; }
        public DateTime? added { get; set; }
        public int productId { get; set; }
        public Product product { get; set; }
        
    }
}
