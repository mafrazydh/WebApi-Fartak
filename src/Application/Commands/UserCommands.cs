using Application.Models.Users;
using Application.Wrappers;
using Domin.Entities;
using MediatR;


namespace Application.Commands
{
    //برای استفاده در هندلر ها با استفاده از مدیاتور
   public record CreateUserCommand(UserDto dto) : IRequest<Result<string>>;
   public record UpdateUserCommand(string id,UserDto dto) : IRequest<User>;
   public record DeleteUserCommand(string id) : IRequest<List<User>>;

}
