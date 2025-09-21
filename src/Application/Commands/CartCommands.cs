using Domin.Entities;
using MediatR;


namespace Application.Commands
{

   public record AddToCartCommand(string userId, string productId, int quantity) : IRequest;
   public record DeleteFromCartCommand(string userId, string productId) : IRequest<List<Cart>>;
   public record UpdateCartQuantityCommand(string userId, string productId, int newQuantity) : IRequest<List<Cart>>;   

}
