using MediatR;

namespace ClaimProcessing.Shared.Suppliers.Commands.UpdateSupplier
{

    public class UpdateSupplierCommand : IRequest<int>
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string ContactPerson { get; set; }
    }
}
