
using ClaimProcessing.Infrastructure;
using ClaimProcessing.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
options.AddPolicy(name: "MyAllowSpecificOrgins",
builder =>
{
    //builder.AllowAnyOrigin();
    builder.WithOrigins("https://localhost:7015");
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Claim Processing",
        Version = "v1",
        Description = "The API enables the handling of reported complaints, detected damages or discrepancies in deliveries from suppliers",
        TermsOfService = new Uri("https://example.com"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Michal",
            Email = "kalinowski.mich@gmail.com",
            Url = new Uri("https://example.com")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com")
        }
    });
    var filePath = Path.Combine(AppContext.BaseDirectory, "ClaimProcessing.Api.xml");
    c.IncludeXmlComments(filePath);
}
);
//builder.Services.AddHealthChecks();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClaimProcessing"));
}


//app.UseHealthChecks("/hc");
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
