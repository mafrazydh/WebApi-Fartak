using Domin.Common;

namespace Domin.Entities
{
    public class Category : BaseEntity
    {
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
