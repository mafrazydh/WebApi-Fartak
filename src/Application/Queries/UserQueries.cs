using Application.Models.Identity;
using Application.Models.Users;
using Application.Wrappers;
using Domin.Entities;
using MediatR;
namespace Application.Queries
{
    //برای استفاده در هندلر ها با استفاده از مدیاتور

    public record AllUsersQuery() : IRequest<List<User>>;
    public record GetUserByIdQuery(string id) : IRequest<User>;
    public record GetTokenQuery(GetTokenDto Dto) : IRequest<Result<JwtResultDto>>;
    public record UserRolesQuery(string id) : IRequest<List<Role>>;


}
