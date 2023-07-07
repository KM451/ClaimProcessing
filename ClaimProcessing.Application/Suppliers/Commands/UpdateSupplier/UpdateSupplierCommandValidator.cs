using FluentValidation;

namespace ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {
            RuleFor(a => a.SupplierId).NotNull().GreaterThan(0);
            RuleFor(a => a.Name).NotEmpty().MaximumLength(80);
            RuleFor(a => a.Street).NotEmpty().MaximumLength(40);
            RuleFor(a => a.City).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Country).NotEmpty().MaximumLength(40);
            RuleFor(a => a.ZipCode).NotEmpty().MaximumLength(10);
            RuleFor(a => a.ContactPerson).NotEmpty().MaximumLength(50);
        }
    }
}
