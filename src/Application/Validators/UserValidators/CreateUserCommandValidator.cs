using Application.Commands;
using FluentValidation;


namespace Application.Validators.UserValidators
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.dto)
                .NotEmpty()
                .SetValidator(new UserDtoValidator())
                .WithName("مدل");
        }
    }
}
