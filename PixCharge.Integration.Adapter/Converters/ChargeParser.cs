using PixCharge.Domain.Transactions.Aggregates;
using PixCharge.Integration.Adapter.Plataform.OpenPix.Models;

namespace PixCharge.Integration.Adapter.Converters;
public class ChargeParser : IParser<Charge, ChargeOpenPix>, IParser<ChargeOpenPix, Charge>
{
    public ChargeOpenPix Parse(Charge origin)
    {
        if (origin == null) return new ChargeOpenPix();
        var chargeOpenPix = new ChargeOpenPix();

        if (origin.Customer != null)
        {
            chargeOpenPix.Customer = new CustomerOpenPix
            {
                Name = origin.Customer.Name,
                TaxID = origin.Customer.TaxID,
                Phone = origin.Customer.Phone,
                CorrelationID = origin.Customer.CorrelationID.ToString()
            };

            chargeOpenPix.Customer.Address = new AddressOpenPix
            {
                Zipcode = origin.Customer.Address.Zipcode,
                Street = origin.Customer.Address.Street,
                Number = origin.Customer.Address.Number,
                Neighborhood = origin.Customer.Address.Neighborhood,
                City = origin.Customer.Address.City,
                State = origin.Customer.Address.State,
                Complement = origin.Customer.Address.Complement,
                Country = origin.Customer.Address.Country
            };
        }
        chargeOpenPix.Value = (int)(origin.Value * 100);
        chargeOpenPix.Identifier = origin.Identifier;
        chargeOpenPix.CorrelationID = origin.CorrelationId.ToString();
        chargeOpenPix.PaymentLinkID = origin.PaymentLinkId;
        chargeOpenPix.TransactionID = origin.TransactionId;
        chargeOpenPix.Status = origin.Status;
        chargeOpenPix.Discount = origin.Discount;
        chargeOpenPix.ValueWithDiscount = origin.ValueWithDiscount;
        chargeOpenPix.ExpiresDate = origin.ExpiresDate;
        chargeOpenPix.Type = origin.Type;
        chargeOpenPix.CreatedAt = origin.CreatedAt;
        chargeOpenPix.UpdatedAt = origin.UpdatedAt;
        chargeOpenPix.BrCode = origin.BrCode;
        chargeOpenPix.ExpiresIn = origin.ExpiresIn;
        chargeOpenPix.PixKey = origin.PixKey;
        chargeOpenPix.PaymentLinkUrl = origin.PaymentLinkUrl;
        chargeOpenPix.QrCodeImage = origin.QrCodeImage;
        chargeOpenPix.GlobalID = origin.GlobalId;
        return chargeOpenPix;
    }

    public  Charge Parse(ChargeOpenPix origin)
    {
        if (origin == null) return new Charge();
        return new Charge
        {
            /*
            Customer =
            {
                Name = origin.Customer?.Name,
                TaxID = origin?.Customer?.TaxID,
                Email = origin.Customer?.Email,
                Phone = origin.Customer?.Phone,
                CorrelationID = new Guid(origin.Customer?.CorrelationID),
                Address =
                {
                    Zipcode = origin.Customer?.Address.Zipcode,
                    Street = origin.Customer?.Address.Street,
                    Number = origin.Customer?.Address.Number,
                    Neighborhood = origin.Customer?.Address.Neighborhood,
                    City = origin.Customer?.Address.City,
                    State = origin.Customer?.Address.State,
                    Complement = origin.Customer.Address.Complement,
                    Country = origin.Customer.Address.Country
                }
            },*/
            Value = origin.Value/100,
            Identifier = origin.Identifier,
            CorrelationId = new Guid(origin.CorrelationID.ToString()),
            PaymentLinkId = origin.PaymentLinkID,
            TransactionId = origin.TransactionID,
            Status = origin.Status,
            Discount = origin.Discount,
            ValueWithDiscount = origin.ValueWithDiscount,
            ExpiresDate = origin.ExpiresDate,
            Type = origin.Type,
            CreatedAt = origin.CreatedAt,
            UpdatedAt = origin.UpdatedAt,
            BrCode = origin.BrCode,
            ExpiresIn = origin.ExpiresIn,
            PixKey = origin.PixKey,
            PaymentLinkUrl = origin.PaymentLinkUrl,
            QrCodeImage = origin.QrCodeImage,
            GlobalId = origin.GlobalID
        };
    }

    public List<ChargeOpenPix> ParseList(List<Charge> origin)
    {
        throw new NotImplementedException();
    }

    public List<Charge> ParseList(List<ChargeOpenPix> origin)
    {
        throw new NotImplementedException();
    }
}
