using Application.Models.Orders;
using MediatR;


namespace Application.Commands
{ 
   public record AddOrderFromCartsCommand(string userId) : IRequest<OrderDto>;
   public record DeleteOrderCommand(string orderId) : IRequest;
   public record ChangeOrderStateCommand(string orderId) : IRequest<OrderDto>;
 }
