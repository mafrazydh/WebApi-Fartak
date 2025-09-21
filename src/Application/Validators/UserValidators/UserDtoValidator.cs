using Application.Models.Users;
using FluentValidation;

namespace Application.Validators.UserValidators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;//یعنی اگر خطایی بود دیگر ادامه نده
            RuleFor(dto => dto.Email)
           .NotEmpty().WithMessage("ایمیل نمی‌تواند خالی باشد.")
           .EmailAddress().WithMessage("ایمیل نامعتبر است.");

            RuleFor(dto => dto.FullName)
          .NotEmpty().WithMessage("نام کامل نمی‌تواند خالی باشد.")
          .MaximumLength(40).WithMessage("بیش از 40 کاراکتر برای نام کامل وارد نکنید")
          .MinimumLength(4).WithMessage("کمتر از 4 کاراکتر برای نام کامل وارد نکنید");

            RuleFor(dto => dto.PhoneNumber)
          .NotEmpty().WithMessage("شماره همراه نمی‌تواند خالی باشد.")
          .MaximumLength(13).WithMessage("بیش از 13 کاراکتر برای شماره همراه وارد نکنید")
          .MinimumLength(10).WithMessage("کمتر از 10 کاراکتر برای شماره همراه وارد نکنید");


        }
    }
}