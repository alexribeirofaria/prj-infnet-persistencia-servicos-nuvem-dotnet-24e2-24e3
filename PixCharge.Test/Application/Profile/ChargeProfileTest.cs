using Application.Transactions.Profile;
using AutoMapper;
using PixCharge.Application.Transactions.Dto;

namespace Application.Profile;
public class ChargeProfileTest
{
    [Fact]
    public void Map_ChargeDto_To_Charge_IsValid()
    {
        // Arrange
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ChargeProfile>());
        var mapper = new Mapper(mapperConfig);

        var chargeDto = new ChargeDto
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            FlatId = Guid.NewGuid(),
            ChargeDate = DateTime.Now,
            ChargeStatus = "Pending",
            BrCode = "123456",
            ExpiresIn = 3600,
            PixKey = "pix123",
            PaymentLinkUrl = "https://paymentlink.com",
            QrCodeImage = "https://qrcode.com"
        };

        // Act
        var charge = mapper.Map<Charge>(chargeDto);

        // Assert
        Assert.NotNull(charge);
        Assert.Equal(chargeDto.Id, charge.Id);
        Assert.Equal(chargeDto.CustomerId, charge.CustomerId);
        Assert.Equal(chargeDto.FlatId, charge.FlatId);
        Assert.Equal(chargeDto.BrCode, charge.BrCode);
        Assert.Equal(chargeDto.ExpiresIn, charge.ExpiresIn);
        Assert.Equal(chargeDto.PixKey, charge.PixKey);
        Assert.Equal(chargeDto.PaymentLinkUrl, charge.PaymentLinkUrl);
        Assert.Equal(chargeDto.QrCodeImage, charge.QrCodeImage);
    }

    [Fact]
    public void Map_Charge_To_ChargeDto_IsValid()
    {
        // Arrange
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ChargeProfile>());
        var mapper = new Mapper(mapperConfig);

        var charge = MockCharge.Instance.GetFaker();
        charge.CustomerId = Guid.NewGuid();
        charge.FlatId = Guid.NewGuid();

        // Act
        var chargeDto = mapper.Map<ChargeDto>(charge);

        // Assert
        Assert.NotNull(chargeDto);
        Assert.Equal(charge.Id, chargeDto.Id);
        Assert.Equal(charge.CustomerId, chargeDto.CustomerId);
        Assert.Equal(charge.FlatId, chargeDto.FlatId);
        Assert.Equal(charge.CreatedAt, chargeDto.ChargeDate);
        Assert.Equal(charge.Status.ToString(), chargeDto.ChargeStatus);
        Assert.Equal(charge.BrCode, chargeDto.BrCode);
        Assert.Equal(charge.ExpiresIn, chargeDto.ExpiresIn);
        Assert.Equal(charge.PixKey, chargeDto.PixKey);
        Assert.Equal(charge.PaymentLinkUrl, chargeDto.PaymentLinkUrl);
        Assert.Equal(charge.QrCodeImage, chargeDto.QrCodeImage);
    }
}
