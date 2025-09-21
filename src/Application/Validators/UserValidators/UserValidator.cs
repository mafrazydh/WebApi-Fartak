using Domin.Entities;
using FluentValidation;


namespace Application.Validators.UserValidators
{
    //E11 ایجاد یک ولیدیتور و رجیستر کردن آن از طریق کانفیگور اپلیکیشن و سپس استفاده در ریپازیتوری
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            ClassLevelCascadeMode=CascadeMode.Stop;//یعنی اگر خطایی بود دیگر ادامه نده
            RuleFor(x => x.Email)
                .EmailAddress()
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
