using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Shipments
{
    public class DeleteShipment_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task DeleteShipmentWithGivenId_ShouldReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "2";
            var response = await client.DeleteAsync($"/api/v1/shipments/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}