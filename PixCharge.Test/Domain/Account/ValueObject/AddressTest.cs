namespace Domain.Account;
public class AddressTest
{
    private const int NUMBER_OF_PROPERTIES = 9;
    [Fact]
    public void Should_Create_Address_With_Sucess()
    {
        // Arrange
        var addressMock = MockAddress.Instance.GetFaker();

        // Act
        var address = new Address
        {
            Id = addressMock.Id,
            Zipcode = addressMock.Zipcode,
            Street = addressMock.Street,
            Number = addressMock.Number,
            Neighborhood = addressMock.Neighborhood,
            City = addressMock.City,
            State = addressMock.State,
            Complement = addressMock.Complement,
            Country = addressMock.Country
        };
        Type addressType = typeof(Address);
        var numberOfProperties = addressType.GetProperties().Length;


        // Assert
        Assert.Equal(addressMock.Id, address.Id);
        Assert.Equal(addressMock.Zipcode, address.Zipcode);
        Assert.Equal(addressMock.Street, address.Street);
        Assert.Equal(addressMock.Number, address.Number);
        Assert.Equal(addressMock.Neighborhood, address.Neighborhood);
        Assert.Equal(addressMock.City, address.City);
        Assert.Equal(addressMock.State, address.State);
        Assert.Equal(addressMock.Complement, address.Complement);
        Assert.Equal(addressMock.Country, address.Country);
        Assert.Equal(NUMBER_OF_PROPERTIES, numberOfProperties);
    }
}