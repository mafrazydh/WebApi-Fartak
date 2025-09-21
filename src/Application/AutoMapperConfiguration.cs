using Application.Models.Category;
using Application.Models.Orders;
using Application.Models.Products;
using Application.Models.Users;
using AutoMapper;
using Domin.Entities;

namespace Application;

public class AutoMapperConfiguration : Profile
{
    //E12 پکیج مربوطه را نصب میکنیم و اینجا کانفیگش میکنیم
    public AutoMapperConfiguration()
    {

        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Product, ProductDto>()
            .ReverseMap()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember is not string str || !string.IsNullOrWhiteSpace(str)));


        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();


    }
}