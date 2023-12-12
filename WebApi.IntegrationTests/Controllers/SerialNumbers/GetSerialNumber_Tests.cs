using ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.SerialNumbers
{
    public class GetSerialNumber_Tests(CustomWebApplicationFactory<Program> _factory) 
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        
        [Fact]
        public async Task GivenSerialNumberId_ReturnsSerialNumberDetails()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            var response = await client.GetAsync($"/api/v1/serial-numbers/{id}");
            var vm = await Utilities.GetResponseContent<SerialNumberVm>(response);
            response.EnsureSuccessStatusCode();
            vm.ShouldNotBeNull();
        }

        [Fact]
        public async Task GivenNotValidSerialNumberId_Returns204Code()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "10";
            var response = await client.GetAsync($"/api/v1/serial-numbers/{id}");
            var vm = await Utilities.GetResponseContent<SerialNumberVm>(response);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
