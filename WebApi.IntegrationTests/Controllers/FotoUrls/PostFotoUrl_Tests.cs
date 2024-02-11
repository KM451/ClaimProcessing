using ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl;
using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.FotoUrls
{
    public class PostFotoUrl_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenFotoUrl_ReturnsIdValue() 
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateFotoUrlCommand attachment = new()
            {
                ClaimId = 2,
                Path = "test",
            };

            var jsonValue = JsonConvert.SerializeObject(attachment);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/foto-urls", content);

            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            id.ShouldBe("4");
        }
    }
}
