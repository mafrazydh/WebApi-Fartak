using Application.Commands;
using Application.Contracts;
using Application.Wrappers;
using MediatR;


namespace Infrastructure.Handlers.UserHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var value = await userRepository.CreateAsync(request.dto);
            return new Result<string> { Value = value };
        }
    }
}
