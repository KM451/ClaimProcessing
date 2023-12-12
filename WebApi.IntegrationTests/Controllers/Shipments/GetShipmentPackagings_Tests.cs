using ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Shipments
{
    public class GetShipmentPackagings_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task GivenShipmentId_ReturnsListOfPackagings()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var response = await client.GetAsync($"/api/v1/shipments/{id}/Packagings");
            var vm = await Utilities.GetResponseContent<ShipmentPackagingsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Packagings.First().Type.ShouldBe("box");
        }

        [Fact]
        public async Task MissingShipmentId_Returns404HttpCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "";
            var response = await client.GetAsync($"/api/v1/shipments/{id}/Packagings");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenNotExistingShipmentId_ReturnsEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "10";
            var response = await client.GetAsync($"/api/v1/shipments/{id}/Packagings");
            var vm = await Utilities.GetResponseContent<ShipmentPackagingsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Packagings.Count.ShouldBe(0);
        }
    }
}
