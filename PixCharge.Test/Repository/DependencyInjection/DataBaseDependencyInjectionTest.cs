using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixCharge.Repository.DataSeeders;
using PixCharge.Repository;
using PixCharge.Repository.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Repository
{
    public class DataBaseDependencyInjectionTest
    {
        private IConfiguration configuration;

        public DataBaseDependencyInjectionTest() 
        {
           configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .AddJsonFile("appsettings.development.json") 
                .Build();
        }

        [Fact]
        public void Should_CreateDataBaseMsSqlServer()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.CreateDataBaseMsSqlServer(configuration);
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<RegisterContext>();

            // Assert            
            Assert.NotNull(context);
        }

        [Fact]
        public void Should_CreateDataBaseInMemory_And_Add_Context_And_DataSeeder()
        {
            // Arrange
            var services = new ServiceCollection();
            
            // Act
            services.CreateDataBaseInMemory();
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<RegisterContext>();
            var dataSeeder = serviceProvider.GetService<IDataSeeder>();
            dataSeeder.SeedData();

            // Assert
            Assert.NotNull(context);
            Assert.NotNull(dataSeeder);
        }
    }
}
