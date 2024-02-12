using ClaimProcessing.Shared.Suppliers.Commands.CreateSupplier;
using FluentValidation;

namespace ClaimProcessing.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MaximumLength(80);
            RuleFor(a => a.Street).NotEmpty().MaximumLength(40);
            RuleFor(a => a.City).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Country).NotEmpty().MaximumLength(40);
            RuleFor(a => a.ZipCode).NotEmpty().MaximumLength(10);
            RuleFor(a => a.ContactPerson).NotEmpty().MaximumLength(50);
        }
    }
}
