using System.ComponentModel.DataAnnotations;

namespace  PixCharge.Application.Transactions.Dto;
public class ChargeDto
{
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid FlatId { get; set; }
    public DateTime ChargeDate { get; set; }
    public string? ChargeStatus { get; set; }
    public string BrCode { get; set; } = String.Empty;
    public int ExpiresIn { get; set; } = 0;
    public string PixKey { get; set; } = String.Empty;
    public string PaymentLinkUrl { get; set; } = String.Empty;
    public string QrCodeImage { get; set; } = String.Empty;
    
}