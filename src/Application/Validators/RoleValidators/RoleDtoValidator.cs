using Application.Models.Users;
using FluentValidation;

namespace Application.Validators.RoleValidators
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RuleFor(dto => dto.Name)
           .NotEmpty().WithMessage("ایمیل نمی‌تواند خالی باشد.")
           .MaximumLength(14).WithMessage("نام باید کمتر از 14 کاراکتر باشد.")
           .MinimumLength(4).WithMessage("نام باید بیشتر از 4 کاراکتر باشد.");



        }
    }
}