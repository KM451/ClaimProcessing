using ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrl;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.FotoUrls
{
    public class GetFotoUrl_Tests(CustomWebApplicationFactory<Program> _factory) 
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        
        [Fact]
        public async Task GivenFotoUrlId_ReturnsFotoUrlsDetail()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            var response = await client.GetAsync($"/api/v1/foto-urls/{id}");
            var vm = await Utilities.GetResponseContent<FotoUrlVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ShouldNotBeNull();
        }

        [Fact]
        public async Task GivenNotValidFotoUrlId_Returns204Code()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "10";
            var response = await client.GetAsync($"/api/v1/foto-urls/{id}");
            var vm = await Utilities.GetResponseContent<FotoUrlVm>(response);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
