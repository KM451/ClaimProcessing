using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using Newtonsoft.Json;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.FotoUrls
{
    public class PostFotoUrl_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenFotoUrl_ReturnsSuccesStatusCode() 
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateFotoUrlCommand foto = new()
            {
                ClaimId = 2,
                FileName = "test.jpg",
                Content = new byte[] {0}
            };

            var jsonValue = JsonConvert.SerializeObject(foto);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/foto-urls", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
