using ClaimProcessing.Application.Claims.Commands.UpdateShipmentId;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class PatchShipmentId_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PatchGivenShipmentId_ReturnsUpdatedClaimIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var claimId = 1;
            var shipmentId = 1;
            var content = new StringContent("");

            var response = await client.PatchAsync($"/api/v1/claims/{claimId}/ShipmentId?shipmentId={shipmentId}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PatchShipmentIdInNotExistingClaim_GeneratesExMessage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var claimId = 9;
            var shipmentId = 1;
            var content = new StringContent("");

            try
            {
                var response = await client.PatchAsync($"/api/v1/claims/{claimId}/ShipmentId?shipmentId={shipmentId}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe($"Object with given Id number '{claimId}' is not found.");
            }
        }
    }
}