using Application.Commands;
using FluentValidation;

namespace Application.Validators.ProductValidators
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.dto)
                .NotEmpty()
                .SetValidator(new ProductDtoValidator())
                .WithName("مدل");
        }
    }
}
