using Application.Models.Category;
using FluentValidation;

namespace Application.Validators.CategoryValidators
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop ;


            RuleFor(dto => dto.CategoryName)
            .NotEmpty().WithMessage("نام دسته نمی‌تواند خالی باشد.")
            .MaximumLength(50).WithMessage("بیش از 50 کاراکتر برای نام دسته وارد نکنید");


            RuleFor(dto => dto.CategoryDescription)
           .NotEmpty().WithMessage("توضیحات دسته نمی‌تواند خالی باشد.")
           .MaximumLength(500).WithMessage("بیش از 500 کاراکتر برای توضیحات دسته وارد نکنید");




        }
    }
}