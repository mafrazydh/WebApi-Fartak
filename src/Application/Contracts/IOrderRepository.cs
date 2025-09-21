using Application.Models.Orders;
using Domin.Entities;


namespace Application.Contracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
      
        Task<OrderDto> AddOrderFromCarts(string userId);
        Task<List<Order>> AllUserOrder(string userId);
        Task DeleteOrder(string orderId);
        Task<OrderDto> ChangeOrderState(string orderId);
        Task<List<Order>> AllOrders();
    }

}
