using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.RoleHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Role>
    {
        private readonly IRoleRepository roleRepository;

        public GetProductByIdQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await roleRepository.GetRoleById(request.id);
        }
    }
}
