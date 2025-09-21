using Application.Commands;
using Application.Contracts;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.UserHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, List<User>>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<List<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.DeleteUser(request.id);
        }
    }
}
