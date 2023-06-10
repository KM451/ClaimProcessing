using MediatR;

namespace ClaimProcessing.Application.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommand : IRequest
    {
        public int SupplierId { get; set; }
    }
}
