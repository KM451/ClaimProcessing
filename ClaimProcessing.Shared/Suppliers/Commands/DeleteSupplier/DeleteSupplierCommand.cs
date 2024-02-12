using MediatR;

namespace ClaimProcessing.Shared.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommand : IRequest
    {
        public int SupplierId { get; set; }
    }
}
