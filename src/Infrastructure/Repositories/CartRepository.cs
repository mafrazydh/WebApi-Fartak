using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domin.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CartRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        
        public async Task AddToCart(string userId, string productId, int quantity)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                throw new CustomException("کاربری با این آیدی وجود ندارد");
            }
            var produc = context.Products.Find(new Guid(productId));
            if (produc == null)
            {
                throw new CustomException("محصولی با این آیدی وجود ندارد");
            }
            var existingItem = context.Carts
                .FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == produc.Id);

            if (existingItem != null)
            {
                throw new CustomException("این محصول در سبد وجود دارد");
            }
            else if (produc.Number < quantity) { 
                throw new CustomException("تعدادی که برای محصول وارد کردید در انبار موجود نمیباشد");
            }
            else
            {

                var cartItem = new Cart
                {
                    UserId = userId,
                    ProductId = produc.Id,
                    Quantity = quantity,
                    IsOrdred = false
                };
                produc.Number = produc.Number - quantity;
                context.Products.Update(produc);
                await AddAsync(cartItem);

            }

        }

        public async Task<List<Cart>> GetCartAsync(string userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                throw new CustomException("کاربری با این آیدی وجود ندارد");
            }

            var result = await context.Carts
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId && ci.IsOrdred == false)
                .ToListAsync();
            if (result.Count != 0)
            {
                return result;
            }
            else
            {
                throw new CustomException("سبد خرید شما خالیست");

            }

        }

        public async Task DeleteFromCart(string userId, string productId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                throw new CustomException("کاربری با این آیدی وجود ندارد");
            }
            var produc = context.Products.Find(new Guid(productId));
            if (produc == null)
            {
                throw new CustomException("محصولی با این آیدی وجود ندارد");
            }

            var cartItem = context.Carts
                .FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == produc.Id);

            if (cartItem != null)
            {
                produc.Number = produc.Number + cartItem.Quantity;
                context.Update(produc);
                context.Carts.Remove(cartItem);
            }
            else{
                throw new CustomException("این محصول در سبد موجود نیست");
            }
            await context.SaveChangesAsync();

        }

        public async Task UpdateCartQuantity(string userId, string productId, int newQuantity)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                throw new CustomException("کاربری با این آیدی وجود ندارد");
            }
            var produc = context.Products.Find(new Guid(productId));
            if (produc == null)
            {
                throw new CustomException("محصولی با این آیدی وجود ندارد");
            }

            var cartItem = await context.Carts
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == produc.Id);

            if (cartItem != null&&produc.Number >= newQuantity)
            {
                cartItem.Quantity = newQuantity;
                produc.Number = produc.Number - newQuantity;
                context.Update(produc);
                context.Update(cartItem);
                await context.SaveChangesAsync();
            }
            else{
                throw new CustomException("تعدادی که برای محصول وارد کردید در انبار موجود نمیباشد");
            }
        }


    }
}
