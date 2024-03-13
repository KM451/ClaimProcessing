using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class PatchClaimStatus_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PatchGivenClaimStatus_ReturnsValueOfNewClaimStatus()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var id = "1";
            var status = "9";
            var content = new StringContent("");
            
            var response = await client.PatchAsync($"/api/v1/claims/{id}/ClaimStatus?claimStatus={status}",content);

            response.EnsureSuccessStatusCode();
            var stringResponse = await Utilities.GetResponseContent<string>(response);
            stringResponse.ShouldBe(status.ToString());
        }

        [Fact]
        public async Task PatchClaimStatusByMinusOneValue_TakesRightStatusValueFromExternalApi()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var id = "1";
            var status = "-1";
            var content = new StringContent("");

            var response = await client.PatchAsync($"/api/v1/claims/{id}/ClaimStatus?claimStatus={status}", content);

            response.EnsureSuccessStatusCode();
            var stringResponse = await Utilities.GetResponseContent<string>(response);
            stringResponse.ShouldBe("3");
        }

        [Fact]
        public async Task PatchClaimStatusInNotExistingClaim_GeneratesExMessage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = "9";
            var status = "1";
            var content = new StringContent("");
  
            try
            {
                var response = await client.PatchAsync($"/api/v1/claims/{id}/ClaimStatus?claimStatus={status}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe($"Object with given Id number '{id}' is not found.");
            }
        }
    }
}
