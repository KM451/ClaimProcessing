using ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls;
using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class GetClaimAttachmentUrls_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        [Fact]
        public async Task GivenClaimId_ReturnsRelatedAttachentUrls()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "1";
            var response = await client.GetAsync($"/api/v1/claims/{id}/AttachmentUrls");
            var vm = await Utilities.GetResponseContent<ClaimAttachmentUrlsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.AttachmentUrls.Count.ShouldBe(2);
        }

        [Fact]
        public async Task MissingClaimId_Returns404HttpCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "";
            var response = await client.GetAsync($"/api/v1/claims/{id}/AttachmentUrls");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenNotExistingClaimId_ReturnsEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "10";
            var response = await client.GetAsync($"/api/v1/claims/{id}/AttachmentUrls");
            var vm = await Utilities.GetResponseContent<ClaimAttachmentUrlsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.AttachmentUrls.Count.ShouldBe(0);
        }
    }
}
