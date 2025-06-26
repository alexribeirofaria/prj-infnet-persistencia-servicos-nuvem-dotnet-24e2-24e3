using Bogus;
using PixCharge.Application.Transactions.Dto;

namespace __mock__;
public class MockCharge
{
    private static readonly object LockObject = new object();
    private static MockCharge? _instance;

    public static MockCharge Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockCharge();
            }
        }
    }

    private MockCharge() { }

    public Charge GetFaker()
    {

        Charge? fakeCharge = new Faker<Charge>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.CorrelationId, f => f.Random.Guid())
        .RuleFor(c => c.Customer, MockCustomer.Instance.GetFaker)
        .RuleFor(c => c.Value, f => new Monetary(f.Finance.Amount()))
        .RuleFor(c => c.Identifier, f => f.Random.Word())
        .RuleFor(c => c.PaymentLinkId, f => f.Random.Word())
        .RuleFor(c => c.TransactionId, f => f.Random.Guid().ToString())
        .RuleFor(c => c.Status, f => f.Random.Word())
        .RuleFor(c => c.Discount, f => f.Random.Int())
        .RuleFor(c => c.ValueWithDiscount, f => f.Random.Int())
        .RuleFor(c => c.ExpiresDate, f => f.Date.Future())
        .RuleFor(c => c.Type, f => f.Random.Word())
        .RuleFor(c => c.CreatedAt, f => f.Date.Past())
        .RuleFor(c => c.UpdatedAt, f => f.Date.Past())
        .RuleFor(c => c.BrCode, f => f.Random.Word())
        .RuleFor(c => c.ExpiresIn, f => f.Random.Int())
        .RuleFor(c => c.PixKey, f => f.Random.Word())
        .RuleFor(c => c.PaymentLinkUrl, f => f.Internet.Url())
        .RuleFor(c => c.QrCodeImage, f => f.Image.LoremPixelUrl())
        .RuleFor(c => c.GlobalId, f => f.Random.Word())
        .Generate();
        return fakeCharge;
    }

    public List<Charge> GetListFaker(int count)
    {
        var chargeList = new List<Charge>();
        for (var i = 0; i < count; i++)
        {
            chargeList.Add(GetFaker());
        }
        return chargeList;
    }

    public ChargeDto GetDtoFromCharge(Charge charge)
    {
        ChargeDto? fakeChargeDto = new Faker<ChargeDto>()
            .RuleFor(c => c.Id, f => charge.Id)
            .RuleFor(c => c.CustomerId, f => charge.CustomerId)
            .RuleFor(c => c.FlatId, f => charge.FlatId)
            .RuleFor(c => c.ChargeDate, f => charge.CreatedAt)
            .RuleFor(c => c.ChargeStatus, f => charge.Status)
            .RuleFor(c => c.BrCode, f => charge.BrCode)
            .RuleFor(c => c.ExpiresIn, f => charge.ExpiresIn)
            .RuleFor(c => c.PixKey, f => charge.PixKey)
            .RuleFor(c => c.PaymentLinkUrl, f => charge.PaymentLinkUrl)
            .RuleFor(c => c.QrCodeImage, f => charge.QrCodeImage)
            .Generate();
        return fakeChargeDto;
    }

    public List<ChargeDto> GetDtoListFromChargeList(List<Charge> charges)
    {
        var chargeDtoList = new List<ChargeDto>();

        foreach (var charge in charges)
        {
            var chargeDto = GetDtoFromCharge(charge);
            chargeDtoList.Add(chargeDto);
        }
        return chargeDtoList;
    }
}