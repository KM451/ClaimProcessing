using ClaimProcessing.Domain.Common;

namespace ClaimProcessing.Domain.Entities
{
    public class FotoUrl : AuditableEntity
    {
        public string Path { get; set; }
        public int ClaimId { get; set; }
        public ICollection<Claim> Claims { get; set; }

    }
}
