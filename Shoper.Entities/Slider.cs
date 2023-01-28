using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class Slider
    {
        public int sliderId { get; set; }
        public byte[] sliderImage { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int? categoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
