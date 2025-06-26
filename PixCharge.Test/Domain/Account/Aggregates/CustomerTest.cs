namespace Domain.Account;
public class CustomerTest
{
    [Fact]
    public void Should_Create_Account_With_Sucess()
    {
        // Arrange
        var customerMock = MockCustomer.Instance.GetFaker();
        var customer = new Customer();

        // Act
        customer.CreateAccount(customerMock);

        // Assert
        Assert.Equal(customerMock.Name, customer.Name);
        Assert.Equal(customerMock.CPF, customer.CPF);
        Assert.Equal(customerMock.Birth, customer.Birth);
        Assert.Equal(customerMock.Phone, customer.Phone);
        Assert.Equal(customerMock.Address, customer.Address);
        Assert.Equal(customerMock.User.Login, customer.User.Login);
    }
}