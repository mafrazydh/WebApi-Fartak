using Domin.Common;
using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Orders
{
    public class OrderDto
    {
        public ICollection<Cart> Carts { get; set; }
        public required string CreatedDate { get; set; }
        public required string OrderState { get; set; }
        public required string ChangedLastDate { get; set; }
        public required string UserId { get; set; }
    }
}
