using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;

namespace Infrastructure.Handlers.UserHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserById(request.id);
        }
    }
}
