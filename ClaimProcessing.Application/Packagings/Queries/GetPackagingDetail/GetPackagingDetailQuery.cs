using MediatR;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingDetail
{
    public class GetPackagingDetailQuery : IRequest<PackagingDetailVm>
    {
        public int PackagingId { get; set; }
    }
}
