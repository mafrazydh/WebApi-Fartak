using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.RoleHandlers
{
    public class AllRoleQueryHandler : IRequestHandler<AllRolesQuery, List<Role>>
    {
        private readonly IRoleRepository roleRepository;

        public AllRoleQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public async Task<List<Role>> Handle(AllRolesQuery request, CancellationToken cancellationToken)
        {
            return await roleRepository.AllRoles();
        }
    }
}
