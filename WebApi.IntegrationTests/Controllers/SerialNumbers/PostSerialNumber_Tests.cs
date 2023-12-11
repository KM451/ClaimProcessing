using ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.SerialNumbers
{
    public class PostSerialNumber_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenSerialNumber_ReturnsIdValue() 
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateSerialNumberCommand serialNo = new()
            {
                ClaimId = 2,
                Value = "tttt",
            };

            var jsonValue = JsonConvert.SerializeObject(serialNo);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/serial-numbers", content);

            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            id.ShouldBe("4");
        }
    }
}
