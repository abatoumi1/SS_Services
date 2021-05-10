using FluentValidation;
using SportServiceApp.Core.Commands.Products;

namespace SportServiceApp.Core.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
