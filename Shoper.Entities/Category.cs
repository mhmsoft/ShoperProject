namespace Shoper.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImagePath { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}