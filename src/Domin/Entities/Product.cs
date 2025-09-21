using Domin.Common;

namespace Domin.Entities
{
    public class Product : BaseEntity
    {

        public required string Name { get; set; }
        public required string Model { get; set; }
        public required string MadeIn { get; set; }
        public required string Price { get; set; }
        public required string Color { get; set; }
        public required int Number { get; set; }
        public required string ProductPictures { get; set; }
        public required string ReleaseDate { get; set; }
        public required string MainDescription { get; set; }
        public required string AllDescription { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
