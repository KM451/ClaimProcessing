using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaim;
using ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Shared.Claims.Queries.GetClaimDetail;

namespace ClaimProcessing.Client.HttpRepository.Interfaces
{
    public interface IClaimsHttpRepository
    {
        Task Add(CreateClaimCommand command);
        Task Edit(UpdateClaimCommand command);
        Task<AllClaimsShortVm> GetAll();
        Task<ClaimDetailVm> GetDetails(int id);
        Task Delete(int id);
    }
}
