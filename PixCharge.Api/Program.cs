using PixCharge.Api.CommonInjectDependence;
using PixCharge.Repository.DataSeeders;
using PixCharge.Repository.DependencyInjection;
using PixCharge.Application.CommonInjectDependence;

var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.EnvironmentName == "DatabaseInMemory")
{
    builder.Services.CreateDataBaseInMemory();
}
else
{
    builder.Services.CreateDataBaseMsSqlServer(builder.Configuration);
}

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Cobrança PIX Version 1",
            Version = "1.0.0"
        });
});

// Autorization Configuratons
builder.Services.AddAuthConfigurations(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapper();

//Repositories
builder.Services.AddRepositories();

//Services
builder.Services.AddServices();


var app = builder.Build();

app.UseHsts();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Cobrança PIX Version 1");
    });
}

/* Configuração para Debug e Deploy de Containers Docker/Docker-Compose */
if (app.Environment.IsStaging())
{
    app.Urls.Add("http://0.0.0.0:2000");
    app.Urls.Add("https://0.0.0.0:2001");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dataSeeder = services.GetRequiredService<IDataSeeder>();
    dataSeeder.SeedData();
}

app.Run();