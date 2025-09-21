using Application.Commands;
using Application.Contracts;
using MediatR;


namespace Infrastructure.Handlers.RoleHandlers
{
    public class DeleteRoleFromUserCommandHandler : IRequestHandler<DeleteRoleFromUserCommand, string>
    {
        private readonly IRoleRepository roleRepository;

        public DeleteRoleFromUserCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public Task<string> Handle(DeleteRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            return roleRepository.DeleteRoleFromUser(request.userId,request.roleId);
        }
    }
}
