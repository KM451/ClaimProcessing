using ClaimProcessing.Domain.Common;
using ClaimProcessing.Domain.ValueObjects;

namespace ClaimProcessing.Domain.Entities
{
    public class Supplier : AuditableEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }       
        public FullName? ContactPerson { get; set; }
        public ICollection<Claim> Claims { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
          
    }
}
