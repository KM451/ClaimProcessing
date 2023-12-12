using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class DeleteSupplier_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task DeleteSupplierWithGivenId_ShouldReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "2";
            var response = await client.DeleteAsync($"/api/v1/suppliers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
