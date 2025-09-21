using Application.Commands;
using Application.Contracts;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.UserHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.UpdateAsync(request.id,request.dto);
        }
    }
}
