using ClaimProcessing.Domain.Common;

namespace ClaimProcessing.Domain.Entities
{
    public class SerialNumber : AuditableEntity
    {
        public string Value { get; set; }
        public int ClaimId { get; set; }
        public ICollection<Claim> Claims { get; set;}
    }
}
