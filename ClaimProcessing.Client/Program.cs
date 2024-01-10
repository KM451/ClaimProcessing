using ClaimProcessing.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("api")
    .AddHttpMessageHandler(sp => sp.GetService<AuthorizationMessageHandler>()
    .ConfigureHandler(new[] { "https://localhost:7063/" }, new[] { "api1" }));

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));

builder.Services.AddOidcAuthentication(options => builder.Configuration.Bind("oidc", options.ProviderOptions));

await builder.Build().RunAsync();
