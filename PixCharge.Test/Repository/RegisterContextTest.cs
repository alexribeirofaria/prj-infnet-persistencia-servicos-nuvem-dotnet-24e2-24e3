using PixCharge.Repository;

namespace Repository;
public class RegisterContextTest
{

    [Fact]
    public void Should_Have_DbSets_RegisterContext()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Act
        using (var context = new RegisterContext(options))
        {
            // Assert
            Assert.NotNull(context.Customer);
            Assert.NotNull(context.Transaction);            
        }
    }

    [Fact]
    public void Should_Apply_Configurations_RegisterContext()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_DataBase_RegisterContext")
            .Options;

        // Act
        using (var context = new RegisterContext(options))
        {
            // Assert
            var model = context.Model;
            Assert.True(model.FindEntityType(typeof(Customer)) != null);
            Assert.True(model.FindEntityType(typeof(Transaction)) != null);
            Assert.True(model.FindEntityType(typeof(Charge)) != null);
        }
    }
}