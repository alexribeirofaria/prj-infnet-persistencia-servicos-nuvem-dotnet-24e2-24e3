using Microsoft.AspNetCore.Mvc;
using Moq;
using PixCharge.Api.Controllers;
using PixCharge.Application;
using PixCharge.Application.Transactions.Dto;

namespace Api.Controllers;
public class ChargeControllerTest
{
    private readonly Mock<IService<ChargeDto>> mockChargeService;
    private readonly ChargeController controller;

    public ChargeControllerTest()
    {
        mockChargeService = new Mock<IService<ChargeDto>>();
        controller = new ChargeController(mockChargeService.Object);
    }

    [Fact]
    public void FindById_Returns_Ok_Result_When_Charge_Found()
    {
        // Arrange
        var charge = MockCharge.Instance.GetFaker();
        var expectedChargeDto = MockCharge.Instance.GetDtoFromCharge(charge);
        mockChargeService.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(expectedChargeDto);

        // Act
        var result = controller.FindById(charge.Id) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<ChargeDto>(result.Value);
        Assert.Equal(expectedChargeDto, result.Value);
    }

    [Fact]
    public void FindById_Returns_NotFound_Result_When_Charge_Not_Found()
    {
        // Arrange
        var chargeId = Guid.NewGuid();
        mockChargeService.Setup(service => service.FindById(chargeId)).Returns((ChargeDto)null);

        // Act
        var result = controller.FindById(chargeId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void FindById_Returns_BadRequest_Result_When_Try_To_FindById()
    {
        // Arrange       
        mockChargeService.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new Exception("BadRequest_Erro_Message"));


        // Act
        var result = controller.FindById(Guid.NewGuid()) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Create_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange
        var charge = MockCharge.Instance.GetFaker();
        var validChargeDto = MockCharge.Instance.GetDtoFromCharge(charge);
        mockChargeService.Setup(service => service.Create(validChargeDto)).Returns(validChargeDto);

        // Act
        var result = controller.Create(validChargeDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<ChargeDto>(result.Value);
        Assert.Equal(validChargeDto, result.Value);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Create((ChargeDto)null) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_When_Try_To_Create()
    {
        // Arrange       
        mockChargeService.Setup(service => service.Create(It.IsAny<ChargeDto>())).Throws(new Exception("BadRequest_Erro_Message"));
        

        // Act
        var result = controller.Create(new()) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
