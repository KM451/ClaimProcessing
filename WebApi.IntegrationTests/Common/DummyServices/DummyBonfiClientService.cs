using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using ClaimProcessing.Application.Common.Interfaces;

namespace WebApi.IntegrationTests.Common.DummyServices
{
    public class DummyBonfiClientService : IBonfiClient
    {
        public async Task<UpdateClaimStatusVm> GetClaim(string searchFilter, CancellationToken cancellationToken)
        {
            var vm = new List<UpdateClaimStatusVm> {new UpdateClaimStatusVm
            {
                data = new BonfiClaim
                {
                    claim = new ClaimData
                    {
                        status = "Work In Progress",
                        subject = "T12/23",
                        claimId = "00032303"
                    }
                }
            }};

            return vm.Where(c => c.data.claim.claimId == searchFilter).FirstOrDefault();
        }

        public Task<string> GetClaims(string searchFilter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
