using Domin.Common;

namespace Domin.Entities
{
    public class Cart:BaseEntity
    {

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsOrdred { get; set; } = false;
        public int Quantity { get; set; }

    }
}
