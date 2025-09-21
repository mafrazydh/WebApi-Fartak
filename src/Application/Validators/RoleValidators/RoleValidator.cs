using Domin.Entities;
using FluentValidation;


namespace Application.Validators.RoleValidators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            ClassLevelCascadeMode=CascadeMode.Stop;
            RuleFor(x => x.Name)
                .MaximumLength(4)
                .MaximumLength(14)
                .NotEmpty();
        }
    }
}
