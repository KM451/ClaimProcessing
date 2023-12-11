using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.SerialNumbers
{
    public class DeleteSerialNumber_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task DeleteSerialNumberWithGivenId_ShouldReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "3";
            var response = await client.DeleteAsync($"/api/v1/serial-numbers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
