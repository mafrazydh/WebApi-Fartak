using Domin.Common;


namespace Domin.Entities
{
    public class Order : BaseEntity
    {
        public ICollection<Cart> Carts { get; set; }
        public string CreatedDate { get; set; }
        public string OrderState { get; set; }
        public string ChangedLastDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
