using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Persistance;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi.IntegrationTests.Common.DummyServices;

namespace WebApi.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            try
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<ICurrentUserService, DummyCurrentUserService>();
                    services.AddScoped<IDateTime, DummyDateTimeService>();

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;

                    var user = scopedServices.GetService<ICurrentUserService>();
                    var dateTime = scopedServices.GetService<IDateTime>();
                    var context = scopedServices.GetRequiredService<ClaimProcessingDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

                    var DbOk = context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            $"database with test messages. Error: {ex.Message}");
                    }
                });
                builder.UseEnvironment("Test");

            }
            catch (Exception ex)
            {
                throw;
            }   
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            var client = CreateClient();
            var token = await GetAccessTokenAsync(client, "alice", "Pass123$");
            client.SetBearerToken(token);
            return client;
        }

        private async Task<string> GetAccessTokenAsync(HttpClient client, string userName, string password)
        {
            var disco = await client.GetDiscoveryDocumentAsync();

            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "openid profile ClaimProcessing.ApiAPI api1",
                UserName = userName,
                Password = password
            });

            if(response.IsError)
            {
                throw new Exception(response.Error);    
            }

            return response.AccessToken;
        }
    }
}   
