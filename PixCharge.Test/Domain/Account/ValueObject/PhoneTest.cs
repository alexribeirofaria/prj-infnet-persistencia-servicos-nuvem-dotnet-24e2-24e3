namespace Domain.Account;
public class PhoneTest
{
    private const int NUMBER_OF_PROPERTIES = 1;
    [Fact]
    public void Should_Create_Phone_With_Sucess()
    {
        // Arrange
        var phoneMock = MockPhone.GetFaker();

        // Act
        var phone = new Phone(phoneMock.Number);

        Type phoneType = typeof(Phone);
        var numberOfProperties = phoneType.GetProperties().Length;

        // Assert
        Assert.Equal(phoneMock.Number, phone.Number);
        Assert.Equal(NUMBER_OF_PROPERTIES, numberOfProperties);
    }

    [Fact]
    public void Should_Throws_Exception_If_Null_Or_Empty_Value()
    {
        // Arrange
        string nullValue = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Phone(nullValue));
        Assert.Contains("Valor do telefone não pode ser nulo ou em branco", exception.Message);

        // Arrange
        string emptyValue = string.Empty;

        // Act & Assert
        exception = Assert.Throws<ArgumentException>(() => new Phone(emptyValue));
        Assert.Contains("Valor do telefone não pode ser nulo ou em branco", exception.Message);
    }

    [Fact]
    public void Phone_Constructor_CreatesInstanceWithValidValue()
    {
        // Arrange
        string phoneMock = MockPhone.GetFaker().Number;

        // Act
        Phone phone = new Phone(phoneMock);

        // Assert
        Assert.Equal(phoneMock, phone.Number);
    }

    [Fact]
    public void Should_Returns_Number_Implicit_Operator_String()
    {
        // Arrange
        string phoneMock = MockPhone.GetFaker().Number;
        Phone phone = new Phone(phoneMock);

        // Act
        string result = phone;

        // Assert
        Assert.Equal(phoneMock, result);
    }

    [Fact]
    public void Should_Returns_Instance_of_Phone_Implicit_Operator()
    {
        // Arrange
        string phoneMock = MockPhone.GetFaker().Number;
        Phone expectedPhone = new Phone(phoneMock);

        // Act
        Phone result = phoneMock;

        // Assert
        Assert.Equal(expectedPhone, result);
    }

}