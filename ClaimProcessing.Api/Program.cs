
using ClaimProcessing.Api;
using ClaimProcessing.Api.Service;
using ClaimProcessing.Application;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Infrastructure;
using ClaimProcessing.Persistance;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");



try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console(outputTemplate: "{Timestamp} {Message}{NewLine:1}{Exception:1}")
       .WriteTo.File("C:\\Temp\\Logs\\log.txt", outputTemplate: "{Timestamp} {Message}{NewLine:1}{Exception:1}")
       .Enrich.FromLogContext()
       .Enrich.WithMachineName()
       .Enrich.WithProcessId()
       .Enrich.WithThreadId() 
       .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPersistance(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyAllowSpecificOrgins", policy => policy.WithOrigins("https://localhost:5001"));
    });

    builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false
        };

    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "api1");
        });
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
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
            c.OAuth2RedirectUrl("https://localhost:7063/swagger/oauth2-redirect.html");
            c.OAuthScopes("api1");
            c.OAuthUsePkce();
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

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


