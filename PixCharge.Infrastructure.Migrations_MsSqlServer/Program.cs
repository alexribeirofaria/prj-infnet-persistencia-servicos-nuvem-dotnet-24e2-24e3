using PixCharge.Repository.DataSeeders;
using PixCharge.Repository.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.CreateDataBaseMsSqlServer(builder.Configuration);
var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dataSeeder = services.GetRequiredService<IDataSeeder>();
        dataSeeder.SeedData();
    }

    app.MapGet("/", () => "Migrations MsSql Server: Execute With Success !");
    app.Run();
 }
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}