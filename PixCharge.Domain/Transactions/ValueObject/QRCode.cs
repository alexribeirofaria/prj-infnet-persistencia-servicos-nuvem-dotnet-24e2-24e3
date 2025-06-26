using PixCharge.Domain.Core.ValueObject;

namespace PixCharge.Domain.Transactions.ValueObject;
public record QRCode
{
    public string Url { get; set; } = String.Empty;
    public string BrCode { get; set; } = String.Empty;
}
