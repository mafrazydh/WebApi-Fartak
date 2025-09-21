using Domin.Entities;
using FluentValidation;
namespace Application.Validators.CategoryValidators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            ClassLevelCascadeMode=CascadeMode.Stop;
            RuleFor(x => x.CategoryName)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.CategoryDescription)
                .MaximumLength(500)
                .NotEmpty();
           
           
        }
    }
}
