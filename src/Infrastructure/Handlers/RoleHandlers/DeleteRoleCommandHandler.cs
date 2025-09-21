using Application.Commands;
using Application.Contracts;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.RoleHandlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, List<Role>>
    {
        private readonly IRoleRepository roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public async Task<List<Role>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await roleRepository.DeleteRole(request.id);
        }
    }
}
