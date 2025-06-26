using PixCharge.Repository.Account.Mapping;

namespace Repository.Mapping;
public class CustomerMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        const int PROPERTY_COUNT = 9;
        // Arrange
        var options = new DbContextOptionsBuilder<MockRegisterContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase_CustomerMapTest")
            .Options;

        using (var context = new MockRegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new CustomerMap();

            configuration.Configure(builder.Entity<Customer>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Customer));
            var propsCount = entityType?.GetNavigations().Count() + entityType?.GetProperties().Count();

            // Act
            var idProperty = entityType?.FindProperty("Id");
            var nameProperty = entityType?.FindProperty("Name");
            //var userProperty = entityType?.FindProperty(nameof(Customer.User));
            var birthProperty = entityType?.FindProperty("Birth");
            var cpfProperty = entityType?.FindProperty("CPF");
            var phoneNavigation = entityType?.FindNavigation(nameof(Customer.Phone));
            var phoneProperty = phoneNavigation?.TargetEntityType.FindProperty(nameof(Phone.Number));

            
            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(nameProperty);
            //Assert.NotNull(userProperty);
            Assert.NotNull(birthProperty);
            Assert.NotNull(cpfProperty);
            Assert.NotNull(phoneNavigation);
            Assert.NotNull(phoneProperty);


            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(nameProperty.IsNullable);
            Assert.Equal(100, nameProperty.GetMaxLength());
            //Assert.False(userProperty.IsNullable);
            Assert.False(birthProperty.IsNullable);
            Assert.False(cpfProperty.IsNullable);
            Assert.Equal(14, cpfProperty.GetMaxLength());
            Assert.False(phoneProperty.IsNullable);
            Assert.Equal(50, phoneProperty.GetMaxLength());
            Assert.Equal(PROPERTY_COUNT, propsCount);
        }
    }
}