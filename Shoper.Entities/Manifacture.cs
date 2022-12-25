using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class Manifacture
    {
        public int ManifactureId { get; set; }
        public string ManifactureName { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
