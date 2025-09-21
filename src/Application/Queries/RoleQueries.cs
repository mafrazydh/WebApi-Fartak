using Domin.Entities;
using MediatR;

namespace Application.Queries
{
    public record AllRolesQuery() : IRequest<List<Role>>;
    public record GetRoleByIdQuery(string id) : IRequest<Role>;
}
