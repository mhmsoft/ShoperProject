using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class ProductItemValue
    {
        public int ItemValueId { get; set; }
        public string Value { get; set; }

        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public ProductItem ProductItem { get; set; }
        public Product Product { get; set; }
    }
}
