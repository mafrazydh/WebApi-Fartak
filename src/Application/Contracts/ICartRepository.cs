
using Domin.Entities;


namespace Application.Contracts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {   
        Task AddToCart(string userId, string productId, int quantity);
        Task<List<Cart>> GetCartAsync(string userId);
        Task DeleteFromCart(string userId, string productId);
        Task UpdateCartQuantity(string userId, string productId, int newQuantity);
       
    }

}
