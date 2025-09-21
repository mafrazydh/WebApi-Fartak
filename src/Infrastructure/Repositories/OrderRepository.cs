using Application.Contracts;
using Application.Exceptions;
using Application.Models.Orders;
using AutoMapper;
using Domin.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
            this.context = context;
        }

      
        public async Task<OrderDto> AddOrderFromCarts(string userId)
        {
            var user = context.Users.Find(userId);
            var order = new Order();

            if (user != null)
            {
                var carts = context.Carts
                    .Where(ci => ci.UserId == userId&& ci.IsOrdred == false).Select(s => new Cart { Id=s.Id,UserId=s.UserId,Product=s.Product,Quantity=s.Quantity} )
                    .ToList();
                foreach (var i in carts)
                {
                    i.IsOrdred = true;
                    context.Carts.Update(i);
                    context.SaveChanges();

                }
                if (carts.Count != 0)
                {
                    order = new Order
                    {
                        Carts = carts,
                        CreatedDate = DateTime.Now.ToString(),
                        ChangedLastDate = DateTime.Now.ToString(),
                        OrderState = "درحال پردازش",
                        UserId = userId
                    };
                    await AddAsync(order);
                    var ordDto = mapper.Map<OrderDto>(order);
                    return ordDto;
                }
                throw new Exception("سبد خرید کاربر خالیست");
            }
            else {
                throw new Exception("چنین کاربری وجود ندارد");
            }
        }


        public async Task<List<Order>> AllUserOrder(string userId)
        {
            var user = context.Users.Find(userId);
            if(user == null)
            {
                throw new CustomException("چنین کاربری وجود ندارد");

            }
            var result = context.Orders
                .Include(ci => ci.Carts)
                .ThenInclude(ci => ci.Product) 
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
            if (result.Result.Count != 0)
            {
                return result.Result;
            }
            else {
                throw new CustomException("لیست سفارشات شما خالیست");

            }
        }

        public async Task<List<Order>> AllOrders()
        {
            
            var result = context.Orders
                .Include(ci => ci.Carts)
                .ThenInclude(ci => ci.Product) 
                .ToListAsync();
            if (result.Result.Count != 0)
            {
                return result.Result;
            }
            else
            {
                throw new CustomException("لیست سفارشات خالیست");

            }
        }

        public async Task DeleteOrder(string orderId)
        {

            var ord = context.Orders
                .Include(ci => ci.Carts)
                .Where(ci => ci.Id == new Guid(orderId)).SingleOrDefault();

            if (ord != null)
            {
                context.Carts.RemoveRange(ord.Carts); 
                context.Orders.Remove(ord);
                context.SaveChanges();
            }
            else { 
                throw new CustomException("سفارشی با این آیدی وجود ندارد");
            }
        }

        public async Task<OrderDto> ChangeOrderState(string orderId)
        {

            var ord = context.Orders
                .Include(ci => ci.Carts).ThenInclude(c => c.Product)
                .Where(ci => ci.Id == new Guid(orderId)).SingleOrDefault();

            if (ord != null)
            {
                if (ord.OrderState == "درحال پردازش")
                {
                    ord.OrderState = "آماده برای ارسال";
                    ord.ChangedLastDate = DateTime.Now.ToString();
                }
                else if (ord.OrderState == "آماده برای ارسال")
                { 
                    ord.OrderState = "ارسال شد";
                    ord.ChangedLastDate = DateTime.Now.ToString();
                }
                else if (ord.OrderState == "ارسال شد")
                {
                    ord.OrderState = "توسط مشتری دریافت گردید";
                    ord.ChangedLastDate = DateTime.Now.ToString();
                }
                else if (ord.OrderState == "توسط مشتری دریافت گردید")
                {
                }
                context.Orders.Update(ord);
                context.SaveChanges();
                var newOrd = mapper.Map<OrderDto>(ord);
                return newOrd;
            } else{
                throw new CustomException("سفارشی با این آیدی وجود ندارد");
            }
        }
    }
}
