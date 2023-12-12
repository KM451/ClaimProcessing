using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.FotoUrls
{
    public class DeleteFotoUrl_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task DeleteFotoUrlWithGivenId_ShouldReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "3";
            var response = await client.DeleteAsync($"/api/v1/foto-urls/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
