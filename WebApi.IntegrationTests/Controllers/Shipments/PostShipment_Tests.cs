using ClaimProcessing.Application.Shipments.Commands.CreateShipment;
using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Shipments
{
    public class PostShipment_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenShipment_ReturnsIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateShipmentCommand shipment = new()
            {
                ShipmentDate = DateTime.Now,
                Speditor = "UPS",
                ShippingDocumentNo = "333",
                TotalWeight = 10,
                SupplierId = 1
            };

            var jsonValue = JsonConvert.SerializeObject(shipment);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/shipments", content);

            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            int.Parse(id).ShouldBeGreaterThan(2);
        }

        [Fact]
        public async Task PostEmptyShipment_Returns400()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateShipmentCommand shipment = new();

            var jsonValue = JsonConvert.SerializeObject(shipment);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/shipments", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }
    }
}