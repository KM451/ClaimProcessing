using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class PatchClaimStatus_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PatchGivenClaimStatus_ReturnsUpdatedClaimIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var id = "1";
            var status = "9";
            var content = new StringContent("");
            
            var response = await client.PatchAsync($"/api/v1/claims/{id}/ClaimStatus?claimStatus={status}",content);

            response.EnsureSuccessStatusCode();
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
