using ClaimProcessing.Shared.Shipments.Commands.UpdateShipment;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Shipments
{
    public class PutShipment_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PutGivenExistingShipment_ReturnsUpdatedShipmentIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateShipmentCommand shipment = new()
            {
                ShipmentId = 1,
                ShipmentDate = new DateTime(2023, 10, 10),
                Speditor = "UPS",
                ShippingDocumentNo = "A1234XYZ",
                TotalWeight = 22,
                SupplierId = 1
            };

            var jsonValue = JsonConvert.SerializeObject(shipment);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/shipments", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            idResponse.ShouldBe(shipment.ShipmentId.ToString());
        }

        [Fact]
        public async Task PutGivenNotExistingShipment_ReturnsIdValueOfNewShipment()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateShipmentCommand shipment = new()
            {
                ShipmentId = 10,
                ShipmentDate = new DateTime(2023, 10, 11),
                Speditor = "UPS",
                ShippingDocumentNo = "A1234XYZ",
                TotalWeight = 22,
                SupplierId = 1
            };

            var jsonValue = JsonConvert.SerializeObject(shipment);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/shipments", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            int.Parse(idResponse).ShouldBeGreaterThan(2);
            int.Parse(idResponse).ShouldBeLessThan(shipment.ShipmentId);
        }
    }
}
