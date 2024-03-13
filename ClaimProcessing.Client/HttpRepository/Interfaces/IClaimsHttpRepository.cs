using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaim;
using ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Shared.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Shared.Claims.Queries.GetClaimFotosUrls;
using Microsoft.AspNetCore.Components.Forms;

namespace ClaimProcessing.Client.HttpRepository.Interfaces
{
    public interface IClaimsHttpRepository
    {
        Task Add(CreateClaimCommand command);
        Task Edit(UpdateClaimCommand command);
        Task<AllClaimsShortVm> GetAll();
        Task<ClaimDetailVm> GetDetails(int id);
        Task Delete(int id);
        Task<ClaimFotoUrlsVm> GetClaimFotoUrls(int id);
    }
}
