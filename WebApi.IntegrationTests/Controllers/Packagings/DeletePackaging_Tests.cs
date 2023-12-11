using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Packagings
{
    public class DeletePackaging_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task DeletePackagingWithGivenId_ShouldReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "2";
            var response = await client.DeleteAsync($"/api/v1/packagings/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}