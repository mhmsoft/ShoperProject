using System.ComponentModel.DataAnnotations;

namespace Shoper.Entities
{
    public class Category
    {
        [Display(Name ="Kategori No")]
        public int CategoryId { get; set; }

        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }

        [Display(Name = "Resim")]
        public string? CategoryImagePath { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}