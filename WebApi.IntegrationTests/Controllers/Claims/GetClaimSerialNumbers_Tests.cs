using ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls;
using ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class GetClaimSerialNumbers_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        [Fact]
        public async Task GivenClaimId_ReturnsRelatedSerialNo()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "1";
            var response = await client.GetAsync($"/api/v1/claims/{id}/SerialNumbers");
            var vm = await Utilities.GetResponseContent<ClaimSerialNumbersVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SerialNumbers.Count.ShouldBe(2);
        }

        [Fact]
        public async Task MissingClaimId_Returns404HttpCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "";
            var response = await client.GetAsync($"/api/v1/claims/{id}/SerialNumbers");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenNotExistingClaimId_ReturnsEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            string id = "10";
            var response = await client.GetAsync($"/api/v1/claims/{id}/SerialNumbers");
            var vm = await Utilities.GetResponseContent<ClaimSerialNumbersVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SerialNumbers.Count.ShouldBe(0);
        }
    }
}
