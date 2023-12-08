using ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class GetClaimFotoUrls_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        [Fact]
        public async Task GivenClaimId_ReturnsRelatedFotoUrls()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "1";
            var response = await client.GetAsync($"/api/v1/claims/{id}/FotoUrls");
            var vm = await Utilities.GetResponseContent<ClaimFotoUrlsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.FotoUrls.Count.ShouldBe(2);
        }

        [Fact]
        public async Task MissingClaimId_Returns404HttpCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "";
            var response = await client.GetAsync($"/api/v1/claims/{id}/FotoUrls");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenNotExistingClaimId_ReturnsEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "10";
            var response = await client.GetAsync($"/api/v1/claims/{id}/FotoUrls");
            var vm = await Utilities.GetResponseContent<ClaimFotoUrlsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.FotoUrls.Count.ShouldBe(0);
        }
    }
}
