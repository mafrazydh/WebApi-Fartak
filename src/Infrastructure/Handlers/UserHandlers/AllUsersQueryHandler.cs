using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;

namespace Infrastructure.Handlers.UserHandlers
{
    public class AllUsersQueryHandler : IRequestHandler<AllUsersQuery, List<User>>
    {
        private readonly IUserRepository userRepository;

        public AllUsersQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<List<User>> Handle(AllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.AllUsers();
        }
    }
}
