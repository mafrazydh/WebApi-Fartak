using Application.Commands;
using FluentValidation;

namespace Application.Validators.RoleValidators
{
    public class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.dto)
                .NotEmpty()
                .SetValidator(new RoleDtoValidator())
                .WithName("مدل");
        }
    }
}
