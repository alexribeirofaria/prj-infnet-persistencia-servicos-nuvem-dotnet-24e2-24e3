using Moq;
using PixCharge.Repository;
using PixCharge.Repository.Persistency;

namespace Repository.Persistency;
public class ChargeRepositoryTest
{
    private Mock<RegisterContext> contextMock;

    public ChargeRepositoryTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase Charge")
            .Options;

        contextMock = new Mock<RegisterContext>(options);
    }

    [Fact]
    public void Save_Should_Add_Charge_And_SaveChanges()
    {
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var mockCharge = MockCharge.Instance.GetFaker();

        // Act
        repository.Save(ref mockCharge);

        // Assert
        contextMock.Verify(c => c.Add(mockCharge), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Update_Should_Update_Charge_And_SaveChanges()
    {
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var mockCharge = MockCharge.Instance.GetFaker();

        // Act
        repository.Update(ref mockCharge);

        // Assert
        contextMock.Verify(c => c.Update(mockCharge), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Delete_Should_Remove_Charge_And_SaveChanges()
    {
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var mockCharge = MockCharge.Instance.GetFaker();

        // Act
        repository.Delete(mockCharge);

        // Assert
        contextMock.Verify(c => c.Remove(mockCharge), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void GetAll_Should_Return_All_Charges()
    {
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var Charges = MockCharge.Instance.GetListFaker(3);
        var dbSetMock = Usings.MockDbSet(Charges);
        contextMock.Setup(c => c.Set<Charge>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.Equal(Charges.Count, result.Count());
    }

    [Fact]
    public void GetById_Should_Return_Charge_With_Correct_Id()
    {
        /*
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var Charge = MockCharge.Instance.GetFaker();
        var ChargeId = Charge.Id;

        contextMock.Setup(c => c.Set<Charge>().Find(ChargeId)).Returns(Charge);

        // Act
        var result = repository.GetById(ChargeId);

        // Assert
        Assert.Equal(Charge, result);
        */
    }

    [Fact]
    public void Find_Should_Return_Charges_Matching_Expression()
    {
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var Charges = MockCharge.Instance.GetListFaker(3);
        var mockCharge = Charges.First();
        var dbSetMock = Usings.MockDbSet(Charges);
        contextMock.Setup(c => c.Set<Charge>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Find(f => f.Id == mockCharge.Id);

        // Assert
        Assert.Single(result);
        Assert.Equal(mockCharge.Id, result.First().Id);
    }

    [Fact]
    public void Exists_Should_Return_True_If_Charges_Match_Expression()
    {
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var Charges = MockCharge.Instance.GetListFaker(3);
        var mockCharge = Charges.First();

        var dbSetMock = Usings.MockDbSet(Charges);
        contextMock.Setup(c => c.Set<Charge>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(f => f.Id == mockCharge.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_Should_Return_False_If_No_Charges_Match_Expression()
    {
        // Arrange
        var repository = new ChargeRepository(contextMock.Object);
        var Charges = MockCharge.Instance.GetListFaker(3);
        var dbSetMock = Usings.MockDbSet(Charges);
        contextMock.Setup(c => c.Set<Charge>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(f => f.Identifier == "Sample Charge");

        // Assert
        Assert.False(result);
    }
}