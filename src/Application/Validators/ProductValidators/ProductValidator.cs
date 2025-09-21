using Domin.Entities;
using FluentValidation;


namespace Application.Validators.ProductValidators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            ClassLevelCascadeMode=CascadeMode.Stop;
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .MinimumLength(4);
                //.NotEmpty();
            RuleFor(x => x.Model)
                .MaximumLength(50);
            //.NotEmpty();
            RuleFor(x => x.Price)
                .MaximumLength(50);
            //.NotEmpty();
            RuleFor(x => x.MadeIn)
                .MaximumLength(50);
            //.NotEmpty();
            RuleFor(x => x.Color)
                .MaximumLength(50);
            //.NotEmpty();
         
           
        }
    }
}
