using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.AttachmentUrls
{
    public class GetAttachmentUrl_Tests(CustomWebApplicationFactory<Program> _factory) 
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        
        [Fact]
        public async Task GivenAttachmentUrlsId_ReturnsAttachmentUrlDetail()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            var response = await client.GetAsync($"/api/v1/attachment-urls/{id}");
            var vm = await Utilities.GetResponseContent<AttachmentUrlVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ShouldNotBeNull();
        }

        [Fact]
        public async Task GivenNotValidAttachmentUrlsId_Returns204Code()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "2";
            var response = await client.GetAsync($"/api/v1/attachment-urls/{id}");
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
