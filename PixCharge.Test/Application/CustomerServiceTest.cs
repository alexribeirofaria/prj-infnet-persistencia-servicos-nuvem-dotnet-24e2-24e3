using AutoMapper;
using Moq;
using PixCharge.Application.Account;
using PixCharge.Application.Account.Dto;
using PixCharge.Repository;
using System.Linq.Expressions;

namespace Application.Service;
public class CustomerServiceTest
{
    private Mock<IMapper> mapperMock;
    private Mock<IRepository<Customer>> customerRepositoryMock;
    private Mock<IRepository<Flat>> flatRepositoryMock;
    private readonly CustomerService customerService;
    private readonly List<Customer> mockCustomerList = MockCustomer.Instance.GetListFaker(3);

    public CustomerServiceTest()
    {
        mapperMock = new Mock<IMapper>();
        customerRepositoryMock = Usings.MockRepositorio(mockCustomerList);
        flatRepositoryMock = Usings.MockRepositorio(new List<Flat>());
                        
        customerService = new CustomerService(
            mapperMock.Object,
            customerRepositoryMock.Object,
            flatRepositoryMock.Object           
        );
    }

    [Fact]
    public void Create_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = MockCustomer.Instance.GetFaker();
        var mockFlat = MockFlat.Instance.GetFaker();
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.User.Login.Email,
            Password = mockCustomer.User.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Address.Zipcode,
                Street = mockCustomer.Address.Street,
                Number = mockCustomer.Address.Number,
                Neighborhood = mockCustomer.Address.Neighborhood,
                City = mockCustomer.Address.City,
                State = mockCustomer.Address.State,
                Complement = mockCustomer.Address.Complement,
                Country = mockCustomer.Address.Country
            },
            FlatId = mockFlat.Id,

        };

        flatRepositoryMock.Setup(repo => repo.GetById(mockFlat.Id)).Returns(mockFlat);
        mapperMock.Setup(mapper => mapper.Map<Address>(customerDto.Address)).Returns(mockCustomer.Address);
        mapperMock.Setup(mapper => mapper.Map<Customer>(customerDto)).Returns(mockCustomer);
        mapperMock.Setup(mapper => mapper.Map<CustomerDto>(It.IsAny<Customer>())).Returns(customerDto);
        

        // Act
        var result = customerService.Create(customerDto);

        // Assert
        customerRepositoryMock.Verify(repo => repo.Exists(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        flatRepositoryMock.Verify(repo => repo.GetById(mockFlat.Id), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<Address>(customerDto.Address), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<CustomerDto>(It.IsAny<Customer>()), Times.Once);
        customerRepositoryMock.Verify(repo => repo.Save(ref It.Ref<Customer>.IsAny), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(customerDto.Name, result.Name);
        Assert.Equal(customerDto.Email, result.Email);
    }

    [Fact]
    public void Create_Customer_With_Existing_Email_Fails()
    {
        // Arrange
        var customerDto = new CustomerDto {  Email = "existing.email@example.com" };

        customerRepositoryMock.Setup(repo => repo.Exists(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(true);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => customerService.Create(customerDto));
        Assert.Equal("Usuário já existente na base.", exception.Message);
        customerRepositoryMock.Verify(repo => repo.Exists(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        flatRepositoryMock.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public void Create_Customer_With_Null_Name()
    {
        // Arrange
        var customerDto = new CustomerDto();

        flatRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Flat)null);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => customerService.Create(customerDto));
        Assert.Equal("Nome não pode ser nulo!", exception.Message);
        customerRepositoryMock.Verify(repo => repo.Save(ref It.Ref<Customer>.IsAny), Times.Never);
    }

    [Fact]
    public void FindAll_Customers_Successfully()
    {
        // Arrange
        var customerDtos = MockCustomer.Instance.GetDtoListFromCustomerList(mockCustomerList);
        var userId = mockCustomerList.First().Id;
        mapperMock.Setup(mapper => mapper.Map<List<CustomerDto>>(It.IsAny<List<Customer>>())).Returns(customerDtos.FindAll(c => c.Id.Equals(userId)));

        // Act
        var result = customerService.FindAll(userId);

        // Assert
        customerRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<List<CustomerDto>>(It.IsAny<List<Customer>>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(mockCustomerList.FindAll(c => c.Id.Equals(userId)).Count, result.Count);
        Assert.All(result, customerDto => Assert.Equal(userId, customerDto.Id));
    }

    [Fact]
    public void FindById_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = mockCustomerList.Last();
        var customerId = mockCustomer.Id;
        mockCustomer.Id = customerId;
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.User.Login.Email,
            Password = mockCustomer.User.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Address.Zipcode,
                Street = mockCustomer.Address.Street,
                Number = mockCustomer.Address.Number,
                Neighborhood = mockCustomer.Address.Neighborhood,
                City = mockCustomer.Address.City,
                State = mockCustomer.Address.State,
                Complement = mockCustomer.Address.Complement,
                Country = mockCustomer.Address.Country
            }
        };

        customerRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(mockCustomer);
        mapperMock.Setup(mapper => mapper.Map<CustomerDto>(It.IsAny<Customer>())).Returns(customerDto);

        // Act
        var result = customerService.FindById(customerId);

        // Assert
        customerRepositoryMock.Verify(repo => repo.GetById(customerId), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<CustomerDto>(mockCustomer), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(mockCustomer.Name, result.Name);
    }

    [Fact]
    public void Update_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = MockCustomer.Instance.GetFaker();
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.User.Login.Email,
            Password = mockCustomer.User.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Address.Zipcode,
                Street = mockCustomer.Address.Street,
                Number = mockCustomer.Address.Number,
                Neighborhood = mockCustomer.Address.Neighborhood,
                City = mockCustomer.Address.City,
                State = mockCustomer.Address.State,
                Complement = mockCustomer.Address.Complement,
                Country = mockCustomer.Address.Country
            }
        };

        mapperMock.Setup(mapper => mapper.Map<Customer>(customerDto)).Returns(mockCustomer);
        customerRepositoryMock.Setup(repo => repo.Update(ref mockCustomer));
        mapperMock.Setup(mapper => mapper.Map<CustomerDto>(mockCustomer)).Returns(customerDto);

        // Act
        var result = customerService.Update(customerDto);

        // Assert
        mapperMock.Verify(mapper => mapper.Map<Customer>(customerDto), Times.Once);
        customerRepositoryMock.Verify(repo => repo.Update(ref mockCustomer), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<CustomerDto>(mockCustomer), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(customerDto.Name, result.Name);
    }

    [Fact]
    public void Delete_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = MockCustomer.Instance.GetFaker();
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.User.Login.Email,
            Password = mockCustomer.User.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Address.Zipcode,
                Street = mockCustomer.Address.Street,
                Number = mockCustomer.Address.Number,
                Neighborhood = mockCustomer.Address.Neighborhood,
                City = mockCustomer.Address.City,
                State = mockCustomer.Address.State,
                Complement = mockCustomer.Address.Complement,
                Country = mockCustomer.Address.Country
            }
        };

        mapperMock.Setup(mapper => mapper.Map<Customer>(customerDto)).Returns(mockCustomer);
        customerRepositoryMock.Setup(repo => repo.Delete(mockCustomer));

        // Act
        var result = customerService.Delete(customerDto);

        // Assert
        mapperMock.Verify(mapper => mapper.Map<Customer>(customerDto), Times.Never);
        customerRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Customer>()), Times.Once);
        Assert.True(result);
    } 
}