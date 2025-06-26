using Moq;
using PixCharge.Repository;
using PixCharge.Repository.Persistency;

namespace Repository.Persistency;
public class PIXRepositoryTest
{
    private Mock<RegisterContext> contextMock;

    public PIXRepositoryTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase PIX")
            .Options;

        contextMock = new Mock<RegisterContext>(options);
    }

    [Fact]
    public void Save_Should_Add_PIX_And_SaveChanges()
    {
        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var mockPIX = MockPIX.Instance.GetFaker();

        // Act
        repository.Save(ref mockPIX);

        // Assert
        contextMock.Verify(c => c.Add(mockPIX), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Update_Should_Update_PIX_And_SaveChanges()
    {
        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var mockPIX = MockPIX.Instance.GetFaker();

        // Act
        repository.Update(ref mockPIX);

        // Assert
        contextMock.Verify(c => c.Update(mockPIX), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Delete_Should_Remove_PIX_And_SaveChanges()
    {
        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var mockPIX = MockPIX.Instance.GetFaker();

        // Act
        repository.Delete(mockPIX);

        // Assert
        contextMock.Verify(c => c.Remove(mockPIX), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void GetAll_Should_Return_All_PIXs()
    {
        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var PIXs = MockPIX.Instance.GetListFaker(3);
        var dbSetMock = Usings.MockDbSet(PIXs);
        contextMock.Setup(c => c.Set<PIX>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.Equal(PIXs.Count, result.Count());
    }

    [Fact]
    public void GetById_Should_Return_PIX_With_Correct_Id()
    {

        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var PIX = MockPIX.Instance.GetFaker();
        var PIXId = PIX.Id;

        contextMock.Setup(c => c.Set<PIX>().Find(PIXId)).Returns(PIX);

        // Act
        var result = repository.GetById(PIXId);

        // Assert
        Assert.Equal(PIX, result);
    }

    [Fact]
    public void Find_Should_Return_PIXs_Matching_Expression()
    {
        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var PIXs = MockPIX.Instance.GetListFaker(3);
        var mockPIX = PIXs.First();
        var dbSetMock = Usings.MockDbSet(PIXs);
        contextMock.Setup(c => c.Set<PIX>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Find(f => f.Id == mockPIX.Id);

        // Assert
        Assert.Single(result);
        Assert.Equal(mockPIX.Id, result.First().Id);
    }

    [Fact]
    public void Exists_Should_Return_True_If_PIXs_Match_Expression()
    {
        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var PIXs = MockPIX.Instance.GetListFaker(3);
        var mockPIX = PIXs.First();

        var dbSetMock = Usings.MockDbSet(PIXs);
        contextMock.Setup(c => c.Set<PIX>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(f => f.Id == mockPIX.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_Should_Return_False_If_No_PIXs_Match_Expression()
    {
        // Arrange
        var repository = new PIXRepository(contextMock.Object);
        var PIXs = MockPIX.Instance.GetListFaker(3);
        var dbSetMock = Usings.MockDbSet(PIXs);
        contextMock.Setup(c => c.Set<PIX>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(f => f.Description == "Sample PIX Description");

        // Assert
        Assert.False(result);
    }
}