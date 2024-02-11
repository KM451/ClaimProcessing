using ClaimProcessing.Shared.Claims.Queries.GetClaimDetail;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class GetClaimDetail_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        [Fact]
        public async Task GivenClaimId_ReturnsClaimDetail()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            var response = await client.GetAsync($"/api/v1/claims/{id}");
            var vm = await Utilities.GetResponseContent<ClaimDetailVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ClaimId.ToString().ShouldBe(id);
        }

        [Fact]
        public async Task GivenWrongClaimId_Returns204()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "10";
            var response = await client.GetAsync($"/api/v1/claims/{id}");
            var vm = await Utilities.GetResponseContent<ClaimDetailVm>(response);
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
