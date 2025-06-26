using AutoMapper;
using Moq;
using PixCharge.Application.Transactions;
using PixCharge.Repository;
using System.Linq.Expressions;

namespace Application.Service;
public class ChargeServiceTest
{
    private Mock<IMapper> mapperMock;
    private Mock<IRepository<Charge>> chargeRepositoryMock;
    private Mock<IRepository<Customer>> cusrtomerRepositoryMock;
    private readonly ChargeService chargeService;
    private readonly List<Charge> mockListCharge = MockCharge.Instance.GetListFaker(5);
    public ChargeServiceTest()
    {
        mapperMock = new Mock<IMapper>();
        chargeRepositoryMock = Usings.MockRepositorio(mockListCharge);
        cusrtomerRepositoryMock = Usings.MockRepositorio(MockCustomer.Instance.GetListFaker(1));
        chargeService = new ChargeService(mapperMock.Object, chargeRepositoryMock.Object, cusrtomerRepositoryMock.Object);
    }
    [Fact]
    public void Should_Create_PIX_Transaction_With_Success()
    {
        // Arrange
        var customer = MockCustomer.Instance.GetFaker();
        var mockTranmsaction  = MockTransaction.Instance.GetFaker();
        customer.Transactions.Add(mockTranmsaction);
        var mockCharges = MockCharge.Instance.GetListFaker(2);
        mockCharges.First().TransactionId = mockTranmsaction.Id.ToString();
        var mockFlat = MockFlat.Instance.GetFaker();
        chargeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Charge, bool>>>())).Returns(mockCharges);               
        chargeRepositoryMock.Setup(repo => repo.Save(ref It.Ref<Charge>.IsAny));       

        // Act
        var charge = chargeService.CreateTransaction(customer, mockFlat, "Teste Unitário Criação de uma transação PIX");
        
        // Assert
        Assert.NotNull(charge);
        chargeRepositoryMock.Verify(repo => repo.Save(ref It.Ref<Charge>.IsAny), Times.Once);
        chargeRepositoryMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Charge, bool>>>()));
    }
}
