namespace Domain.Account;
public class LoginTest
{
    [Fact]
    public void Should_Success_With_Valid_Email()
    {
        // Arrange
        var customer = MockCustomer.Instance.GetFaker();

        // Act
        customer.User.Login.Email = "usuario@teste.com";

        // Assert
        Assert.Equal("usuario@teste.com", customer.User.Login.Email);
    }

    [Fact]
    public void Should_Throws_Erro_With_Invalid_Email()
    {
        // Arrange
        var customer = MockCustomer.Instance.GetFaker();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => customer.User.Login.Email = "Email inválido!");
    }
            
    [Fact]
    public void Should_Throws_Erro_With_Long_Email()
    {
        // Arrange
        var cutomer = MockCustomer.Instance.GetFaker();

        // Act e Assert
        Assert.Throws<ArgumentException>(() => cutomer.User.Login.Email = new string('a', 257));
    }
}