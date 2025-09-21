using Application.Models.Products;
using FluentValidation;

namespace Application.Validators.ProductValidators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop ;

            RuleFor(dto => dto.Name)
            .MaximumLength(100).WithMessage("بیش از 40 کاراکتر برای نام کامل وارد نکنید")
            .MinimumLength(4).WithMessage("کمتر از 4 کاراکتر برای نام کامل وارد نکنید");


            RuleFor(x => x.Price)
                .MaximumLength(50);
            RuleFor(x => x.MadeIn)
                .MaximumLength(50).WithMessage("فیلد کشور سازنده بیش از 50 کاراکتر نباید باشد");
            RuleFor(x => x.Color)
                .MaximumLength(50).WithMessage("فیلد رنگ بیش از 50 کاراکتر نباید باشد");
            RuleFor(x => x.CategoryId.ToString())
                .MaximumLength(50).WithMessage("فیلد دسته بیش از 50 کاراکتر نباید باشد")
                .NotEmpty().WithMessage("لطفا فیلد دسته را خالی نگذارید");
            


        }
    }
}