using ClaimProcessing.Shared.Claims.Queries.GetClaimsUser;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class GetClaimsUser_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task NotSpecifiedFilter_ReturnsSingleClaim()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var response = await client.GetAsync($"/api/v1/claims/user?sort=asc");
            var vm = await Utilities.GetResponseContent<ClaimsUserVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Claims.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GivenFilter_ReturnsSingleClaim()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var filterAndSort = "?filter=ItemCode%20eq%2012A34B&sort=asc";

            var response = await client.GetAsync($"/api/v1/claims/user{filterAndSort}");
            var vm = await Utilities.GetResponseContent<ClaimsUserVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Claims.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GivenWrongFilter_GeneratesExMessage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var filterAndSort = "?filter=BeerType%20eq%20Lech&sort=asc";
            try
            {
                var response = await client.GetAsync($"/api/v1/claims/user{filterAndSort}");
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe("Wartość podana jako filtrowane pole: 'BeerType' jest nieprawidłowa.");
            }
        }
    }
}
