using Application.Commands;
using Application.Contracts;
using MediatR;


namespace Infrastructure.Handlers.RoleHandlers
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, string>
    {
        private readonly IRoleRepository roleRepository;

        public AddRoleToUserCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public Task<string> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            return roleRepository.AddRoleToUser(request.userId,request.roleId);
        }
    }
}
