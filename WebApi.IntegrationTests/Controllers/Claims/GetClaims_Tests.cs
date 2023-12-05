using ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class GetClaims_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task GivenClaimFilter_ReturnsSingleClaim()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var filterAndSort = "?filter=OwnerType%20eq%20o2&sort=asc";

            var response = await client.GetAsync($"/api/v1/claims{filterAndSort}");
            var vm = await Utilities.GetResponseContent<AllClaimsShortVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Claims.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GivenClaimFilter_NotReturnAnyClaim()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var filterAndSort = "?filter=OwnerType%20eq%20o5&sort=asc";

            var response = await client.GetAsync($"/api/v1/claims{filterAndSort}");
            var vm = await Utilities.GetResponseContent<AllClaimsShortVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Claims.Count.ShouldBe(0);
        }

        [Fact]
        public async Task GivenWrongClaimFilter_GeneratesExMessage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var filterAndSort = "?filter=BeerType%20eq%20o5&sort=asc";
            try
            {
                var response = await client.GetAsync($"/api/v1/claims{filterAndSort}");
            }
            catch ( Exception ex )
            {
                ex.Message.ShouldBe("Wartość podana jako filtrowane pole: 'BeerType' jest nieprawidłowa.");
            }
        }

        [Fact]
        public async Task GivenDescendingSort_ReturnsFirstClaimOnLastPosition()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var filterAndSort = "?sort=desc";
            var response = await client.GetAsync($"/api/v1/claims{filterAndSort}");

            var vm = await Utilities.GetResponseContent<AllClaimsShortVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Claims.Last().ClaimId.ShouldBe(1);
        }
    }
}
