using ClaimProcessing.Client;
using ClaimProcessing.Client.HttpRepository;
using ClaimProcessing.Client.HttpRepository.Interfaces;
using ClaimProcessing.Client.Services.Interfaces;
using ClaimProcessing.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "api/");
    client.Timeout = TimeSpan.FromMinutes(5);
})
    .AddHttpMessageHandler(sp => sp.GetService<AuthorizationMessageHandler>()
    .ConfigureHandler(new[] { builder.Configuration["ApiConfiguration:BaseAddress"]}, new[] { "api1" }));


builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));

builder.Services.AddOidcAuthentication(options => builder.Configuration.Bind("oidc", options.ProviderOptions));
builder.Services.AddScoped<IClaimsHttpRepository, ClaimsHttpRepository>();
builder.Services.AddScoped<ISupplierHttpRepository, SupplierHttpRepository>();
builder.Services.AddScoped<IToastrService, ToastrService>();

await builder.Build().RunAsync();
