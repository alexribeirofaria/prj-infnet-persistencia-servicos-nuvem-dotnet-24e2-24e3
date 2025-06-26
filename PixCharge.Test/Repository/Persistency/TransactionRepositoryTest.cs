using Moq;
using PixCharge.Repository;
using PixCharge.Repository.Persistency;

namespace Repository.Persistency;
public class TransactionRepositoryTest
{
    private Mock<RegisterContext> contextMock;

    public TransactionRepositoryTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_TransactionRepositoryTest")
            .Options;

        contextMock = new Mock<RegisterContext>(options);
    }

    [Fact]
    public void Save_Should_Add_Transaction_And_SaveChanges()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var mockTransaction = MockTransaction.Instance.GetFaker();

        // Act
        repository.Save(ref mockTransaction);

        // Assert
        contextMock.Verify(c => c.Add(mockTransaction), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Update_Should_Update_Transaction_And_SaveChanges()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var mockTransaction = MockTransaction.Instance.GetFaker();

        // Act
        repository.Update(ref mockTransaction);

        // Assert
        contextMock.Verify(c => c.Update(mockTransaction), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Delete_Should_Remove_Transaction_And_SaveChanges()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var mockTransaction = MockTransaction.Instance.GetFaker();

        // Act
        repository.Delete(mockTransaction);

        // Assert
        contextMock.Verify(c => c.Remove(mockTransaction), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void GetAll_Should_Return_All_Transactions()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var Transactions = MockTransaction.Instance.GetListFaker(3);
        var dbSetMock = Usings.MockDbSet(Transactions);
        contextMock.Setup(c => c.Set<Transaction>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.Equal(Transactions.Count, result.Count());
    }

    [Fact]
    public void GetById_Should_Return_Transaction_With_Correct_Id()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var Transaction = MockTransaction.Instance.GetFaker();
        var TransactionId = Transaction.Id;

        contextMock.Setup(c => c.Set<Transaction>().Find(TransactionId)).Returns(Transaction);

        // Act
        var result = repository.GetById(TransactionId);

        // Assert
        Assert.Equal(Transaction, result);
    }

    [Fact]
    public void Find_Should_Return_Transactions_Matching_Expression()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var Transactions = MockTransaction.Instance.GetListFaker(3);
        var mockTransaction = Transactions.First();
        var dbSetMock = Usings.MockDbSet(Transactions);
        contextMock.Setup(c => c.Set<Transaction>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Find(f => f.Id == mockTransaction.Id);

        // Assert
        Assert.Single(result);
        Assert.Equal(mockTransaction.Id, result.First().Id);
    }

    [Fact]
    public void Exists_Should_Return_True_If_Transactions_Match_Expression()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var Transactions = MockTransaction.Instance.GetListFaker(3);
        var mockTransaction = Transactions.First();

        var dbSetMock = Usings.MockDbSet(Transactions);
        contextMock.Setup(c => c.Set<Transaction>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(f => f.Id == mockTransaction.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_Should_Return_False_If_No_Transactions_Match_Expression()
    {
        // Arrange
        var repository = new TransactionRepository(contextMock.Object);
        var Transactions = MockTransaction.Instance.GetListFaker(3);
        var dbSetMock = Usings.MockDbSet(Transactions);
        contextMock.Setup(c => c.Set<Transaction>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(f => f.Description == "Invalid Description");

        // Assert
        Assert.False(result);
    }
}