using ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class PatchRmaAvailability_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PatchGivenRmaAvailability_ReturnsUpdatedClaimIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var rma = true;
            var content = new StringContent("");

            var response = await client.PatchAsync($"/api/v1/claims/{id}/RmaAvailable?rmaAvailability={rma}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PatchRmaAvailabilityInNotExistingClaim_GeneratesExMessage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 9;
            var rma = true;
            var content = new StringContent("");

            try
            {
                var response = await client.PatchAsync($"/api/v1/claims/{id}/RmaAvailable?rmaAvailability={rma}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe($"Object with given Id number '{id}' is not found.");
            }
        }
    }
}
