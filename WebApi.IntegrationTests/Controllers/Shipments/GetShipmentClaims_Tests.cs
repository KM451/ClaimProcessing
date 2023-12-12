using ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Shipments
{
    public class GetShipmentClaims_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task GivenShipmentId_ReturnsListOfClaims()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var response = await client.GetAsync($"/api/v1/shipments/{id}/Claims");
            var vm = await Utilities.GetResponseContent<ShipmentClaimsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ShipmentClaims.First().ItemCode.ShouldBe("12A34B");
        }

        [Fact]
        public async Task MissingShipmentId_Returns404HttpCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "";
            var response = await client.GetAsync($"/api/v1/shipments/{id}/Claims");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenNotExistingShipmentId_ReturnsEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "10";
            var response = await client.GetAsync($"/api/v1/shipments/{id}/Claims");
            var vm = await Utilities.GetResponseContent<ShipmentClaimsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ShipmentClaims.Count.ShouldBe(0);
        }
    }
}