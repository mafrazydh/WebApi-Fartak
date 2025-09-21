using Application.Models.Users;
using Application.Wrappers;
using Domin.Entities;
using MediatR;

namespace Application.Commands
{
    public record CreateRoleCommand(RoleDto dto) : IRequest<Result<string>>;
    public record DeleteRoleCommand(string id) : IRequest<List<Role>>;
    public record AddRoleToUserCommand(string userId, string roleId) : IRequest<string>;
    public record DeleteRoleFromUserCommand(string userId, string roleId) : IRequest<string>;
    

}
