using Application.Commands;
using FluentValidation;


namespace Application.Validators.CategoryValidators
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategotyAsyncCommand>
    {
        public CreateCategoryCommandValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.categoryDto)
                .NotEmpty()
                .SetValidator(new CategoryDtoValidator())
                .WithName("مدل");
        }
    }
}
