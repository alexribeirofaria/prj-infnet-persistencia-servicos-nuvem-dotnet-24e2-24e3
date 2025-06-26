using AutoMapper;
using PixCharge.Application.Account.Dto;
using PixCharge.Application.Account.Profile;

namespace Application.Profile;
public class CustomerProfileTest
{

    [Fact]
    public void Map_CustomerDto_To_Customer_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<CustomerProfile>();
        }));

        var customerDto = new CustomerDto
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "password123",            
            CPF = "12345678901",
            Birth = new DateTime(1990, 1, 1),
            Phone = "123456789",
            Address = new AddressDto
            {
                Zipcode = "12345-678",
                Street = "Main Street",
                Number = "123",
                Neighborhood = "Downtown",
                City = "Cityville",
                State = "ST",
                Complement = "Apt 456",
                Country = "Countryland"
            },
            FlatId = Guid.NewGuid(),
        };

        // Act
        var customer = mapper.Map<Customer>(customerDto);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal(customerDto.Id, customer.Id);
        Assert.Equal(customerDto.Name, customer.Name);
        Assert.Equal(customerDto.Email, customer.User.Login.Email);
        Assert.Equal(customerDto.CPF, customer.CPF);
        Assert.Equal(customerDto.Birth, customer.Birth);
        Assert.NotNull(customer.Phone);
        Assert.Equal(customerDto.Phone, customer.Phone?.Number);
        Assert.NotNull(customer.Address);
    }

    [Fact]
    public void Map_Customer_To_CustomerDto_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<CustomerProfile>();
        }));

        var customer = MockCustomer.Instance.GetFaker();
        customer.AddFlat(MockFlat.Instance.GetFaker());

        // Act
        var customerDto = mapper.Map<CustomerDto>(customer);

        // Assert
        Assert.NotNull(customerDto);
        Assert.Equal(customer.Id, customerDto.Id);
        Assert.Equal(customer.Name, customerDto.Name);
        Assert.Equal(customer.User.Login.Email, customerDto.Email);
        Assert.Equal(customer.CPF, customerDto.CPF);
        Assert.Equal(customer.Birth, customerDto.Birth);
        Assert.NotNull(customerDto.Phone);
        Assert.Equal(customer.Phone?.Number, customerDto.Phone);
        Assert.NotNull(customerDto.Address);
        Assert.Equal(customer.Address.Zipcode, customerDto.Address.Zipcode);
        Assert.Equal(customer.Address.Street, customerDto.Address.Street);
        Assert.Equal(customer.Address.Number, customerDto.Address.Number);
        Assert.Equal(customer.Address.Neighborhood, customerDto.Address.Neighborhood);
        Assert.Equal(customer.Address.City, customerDto.Address.City);
        Assert.Equal(customer.Address.State, customerDto.Address.State);
        Assert.Equal(customer.Address.Complement, customerDto.Address.Complement);
        Assert.Equal(customer.Address.Country, customerDto.Address.Country);
    }
}
