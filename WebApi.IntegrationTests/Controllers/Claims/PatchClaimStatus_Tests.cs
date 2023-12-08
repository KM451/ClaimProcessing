using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class PatchClaimStatus_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PatchGivenExistingClaim_ReturnsUpdatedClaimIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateClaimStatusCommand claim = new()
            {
                ClaimId = 1,
                ClaimStatus = 9,
            };

            var jsonValue = JsonConvert.SerializeObject(claim);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"/api/v1/claims/{claim.ClaimId}/ClaimStatus", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PatchNotExistingClaim_GeneratesExMessage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateClaimStatusCommand claim = new()
            {
                ClaimId = 9,
                ClaimStatus = 1,
            };

            var jsonValue = JsonConvert.SerializeObject(claim);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PatchAsync($"/api/v1/claims/{claim.ClaimId}/ClaimStatus", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe($"Object with given Id number '{claim.ClaimId}' is not found.");
            }
        }
    }
}
