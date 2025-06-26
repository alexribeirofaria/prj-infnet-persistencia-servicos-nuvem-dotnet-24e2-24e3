using PixCharge.Application.Transactions.Dto;
using PixCharge.Domain.Transactions.Aggregates;

namespace Application.Transactions.Profile;
public class ChargeProfile : AutoMapper.Profile
{
    public ChargeProfile()
    {
        CreateMap<ChargeDto, Charge>().ReverseMap();

        CreateMap<Charge, ChargeDto>()
            .AfterMap((s, d) => {
                d.ChargeDate = s.CreatedAt;
                d.ChargeStatus = s.Status;
            })
            .ReverseMap();        
    }
}