using ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Shipments
{
    public class GetShipment_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task GivenShipmentId_ReturnsShipmentDetail()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "1";
            var response = await client.GetAsync($"/api/v1/shipments/{id}");
            var vm = await Utilities.GetResponseContent<ShipmentDetailVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ShippingDocumentNo.ShouldBe("A1234XYZ");
        }

        [Fact]
        public async Task GivenWrongShipmentId_Returns204()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "10";
            var response = await client.GetAsync($"/api/v1/shipments/{id}");
            var vm = await Utilities.GetResponseContent<ShipmentDetailVm>(response);
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}

