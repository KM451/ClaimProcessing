using MediatR;

namespace ClaimProcessing.Shared.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; } 
        public string Country { get; set; }
        public string ZipCode { get; set; } 
        public string ContactPerson { get; set; }

    }
}
