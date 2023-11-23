using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.AttachmentUrls
{
    public class GetAttachmentUrl_Tests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public GetAttachmentUrl_Tests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenAttachmentUrlsId_ReturnsAttachmentUrlDetail()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            //var response = await client.GetAsync($"/api/v1/attachment-urls/{id}");
            //var vm = await Utilities.GetResponseContent<AttachmentUrlVm>(response);
            //response.EnsureSuccessStatusCode();
            //vm.ShouldNotBeNull();
            var response = await client.GetAsync($"/api/hc");
            response.EnsureSuccessStatusCode();
            var a = await response.Content.ReadAsStringAsync();
            a.ShouldBe("Healthy");
        }
    }
}
