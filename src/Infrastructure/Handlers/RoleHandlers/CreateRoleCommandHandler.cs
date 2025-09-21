using Application.Commands;
using Application.Contracts;
using Application.Wrappers;
using MediatR;


namespace Infrastructure.Handlers.RoleHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateRoleCommand, Result<string>>
    {
        private readonly IRoleRepository roleRepository;

        public CreateProductCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var value = await roleRepository.CreateAsync(request.dto);
            return new Result<string> { Value = value };
        }
    }
}
