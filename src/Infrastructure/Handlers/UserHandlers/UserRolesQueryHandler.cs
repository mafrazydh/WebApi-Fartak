using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.UserHandlers
{
    public class UserRolesQueryHandler : IRequestHandler<UserRolesQuery, List<Role>>
    {
        private readonly IUserRepository userRepository;

        public UserRolesQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<List<Role>> Handle(UserRolesQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.UserRoles(request.id);
        }
    }
}
