using ClaimProcessing.Application.Packagings.Queries.GetPackaging;
using ClaimProcessing.Shared.Packagings.Queries.GetPackaging;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Packagings
{
    public class GetPackaging_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        [Fact]
        public async Task GivenPackagingId_ReturnsPackagingDetails()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            var response = await client.GetAsync($"/api/v1/packagings/{id}");
            var vm = await Utilities.GetResponseContent<PackagingVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ShouldNotBeNull();
        }

        [Fact]
        public async Task GivenNotValidPackagingId_Returns204Code()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "10";
            var response = await client.GetAsync($"/api/v1/packagings/{id}");
            var vm = await Utilities.GetResponseContent<PackagingVm>(response);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}