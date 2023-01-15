using Shoper.Entities;

namespace Shoper.UI.Models.ViewModels
{
    public class CardItem
    {
        public Guid Id { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
    }
}
