using Domin.Entities;

namespace Application.Models.Category
{
    public class CategoryDto
    {
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
