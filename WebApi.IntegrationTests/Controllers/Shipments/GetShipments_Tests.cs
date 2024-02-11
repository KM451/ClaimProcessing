using ClaimProcessing.Shared.Shipments.Queries.GetShipments;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Shipments
{
    public class GetShipments_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task TestReturnsListOfShipments()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var response = await client.GetAsync($"/api/v1/shipments");
            var vm = await Utilities.GetResponseContent<ShipmentsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Shipments.First().ShipmentDate.ShouldBe(new DateTime(2023, 10, 10));
        }
    }
}
