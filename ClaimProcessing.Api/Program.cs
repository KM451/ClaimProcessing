using ClaimProcessing.Api;
using ClaimProcessing.Api.Service;
using ClaimProcessing.Application;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Infrastructure;
using ClaimProcessing.Infrastructure.Identity;
using ClaimProcessing.Persistance;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting up");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host
        .UseSerilog((ctx, lc) => lc
           .WriteTo.Console(outputTemplate: "{Timestamp} {Message}{NewLine:1}{Exception:1}")
           .WriteTo.File("C:\\Temp\\Logs\\log.txt", outputTemplate: "{Timestamp} {Message}{NewLine:1}{Exception:1}")
           .Enrich.FromLogContext()
           .Enrich.WithMachineName()
           .Enrich.WithProcessId()
           .Enrich.WithThreadId()
           .ReadFrom.Configuration(ctx.Configuration));

    var services = builder.Services;
    var configuration = builder.Configuration;
    var environment = builder.Environment;

    services.AddApplication();
    services.AddInfrastructure(builder.Configuration);
    services.AddPersistance(builder.Configuration);
    services.AddControllers();
    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));

    services.AddCors(options =>
    {
        options.AddPolicy("MyAllowSpecificOrgins", policy => policy.WithOrigins("https://localhost:5001"));
    });

    if (environment.IsEnvironment("Test"))
    {

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ClaimDatabase")));

        services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.ApiResources.Add(new ApiResource("api1"));
                    options.ApiScopes.Add(new ApiScope("api1"));
                    options.Clients.Add(new Client
                    {
                        ClientId = "client",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedScopes = { "api1" }
                    });
                })
                .AddTestUsers(new List<TestUser>
                {
                        new TestUser
                        {
                            SubjectId = "4B434A88-212D-4A4D-A17C-F35102D73CBB",
                            Username = "alice",
                            Password = "Pass123$",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "alice@user.com"),
                                new Claim(ClaimTypes.Name, "alice")
                            }
                        }
                }).AddDeveloperSigningCredential();

        services.AddAuthentication("Bearer").AddIdentityServerJwt();
        
    }
    else
    {
        services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://localhost:5001";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "api1");
            });
        });
    }

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows()
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                    TokenUrl = new Uri("https://localhost:5001/connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        {"api1", "Full access" },
                    }
                }
            }
        });
        c.OperationFilter<AuthorizeCheckOperationFilter>();
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Claim Processing",
            Version = "v1",
            Description = "The API enables the handling of reported complaints, detected damages or discrepancies in deliveries from suppliers",
            TermsOfService = new Uri("https://example.com"),
            Contact = new OpenApiContact
            {
                Name = "Michal",
                Email = "kalinowski.mich@gmail.com",
                Url = new Uri("https://example.com")
            },
            License = new OpenApiLicense
            {
                Name = "License",
                Url = new Uri("https://example.com")
            }
        });
        var filePath = Path.Combine(AppContext.BaseDirectory, "ClaimProcessing.Api.xml");
        c.IncludeXmlComments(filePath);
    }
    );

    var app = builder.Build();

    //Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClaimProcessing");
            c.OAuthClientId("swagger");
            c.OAuthClientSecret("secret");
            c.OAuth2RedirectUrl("https://localhost:7063/swagger/oauth2-redirect.html");
            c.OAuthScopes("api1");
            c.OAuthUsePkce();
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    if (environment.IsEnvironment("Test"))
    {
        app.UseIdentityServer();
    }

    app.UseSerilogRequestLogging();

    app.UseRouting();

    app.UseCors();

    app.UseAuthorization();

    app.MapControllers().RequireAuthorization("ApiScope");

    app.Run();


}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }

