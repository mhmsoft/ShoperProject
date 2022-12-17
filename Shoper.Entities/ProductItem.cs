using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class ProductItem
    {
        [Display(Name="Özellik No")]
        public int ItemId { get; set; }
        [Display(Name = "Özellik")]
        public string ItemName { get; set; }       
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ProductItemValue ProductItemValue { get; set; }
    }
}
