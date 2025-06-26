using PixCharge.Integration.Adapter.Converters;

namespace Adapter;
public class ChargeParserTest
{
    [Fact]
    public void ParseChargeToChargeOpenPix_ShouldReturnCorrectChargeOpenPix()
    {
        // Arrange
        var charge = MockCharge.Instance.GetFaker();
        var parser = new ChargeParser();

        // Act
        var result = parser.Parse(charge);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void ParseChargeOpenPixToCharge_ShouldReturnCorrectCharge()
    {
        // Arrange
        var charge = MockCharge.Instance.GetFaker();
        var chargeOpenPix = new ChargeParser().Parse(charge);
        var parser = new ChargeParser();

        // Act
        var result = parser.Parse(chargeOpenPix);

        // Assert
        Assert.NotNull(result);
    }
}
