using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.AttachmentUrls
{
    public class DeleteAttachmentUrl_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task DeleteAttachmentUrlWithGivenId_ShouldReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "3";
            var response = await client.DeleteAsync($"/api/v1/attachment-urls/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
