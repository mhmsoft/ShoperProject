using Shoper.Entities;

namespace Shoper.Management.Models.ViewModels
{
    public class VMItems
    {
        public IEnumerable<ProductItem> Items { get; set; }
        public IEnumerable<ProductItemValue> ItemValues { get; set; }
    }
}
