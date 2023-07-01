using MediatR;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackaging
{
    public class GetPackagingQuery : IRequest<PackagingVm>
    {
        public int PackagingId { get; set; }
    }
}
