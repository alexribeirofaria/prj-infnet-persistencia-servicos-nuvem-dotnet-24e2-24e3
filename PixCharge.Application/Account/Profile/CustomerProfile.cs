using PixCharge.Application.Account.Dto;
using PixCharge.Domain.Account.ValueObject;
using PixCharge.Domain.Account.Aggregates;

namespace PixCharge.Application.Account.Profile;
public class CustomerProfile : AutoMapper.Profile
{
    public CustomerProfile() 
    {
        CreateMap<CustomerDto, Customer>()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new Phone(src.Phone ?? String.Empty)))
            .ReverseMap();

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Login.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Number))
            .AfterMap((s, d) =>
            {
                d.Password = "********";
                d.FlatId = s.Flats.MaxBy(x => x.DtCreated) != null ? s.Flats.MaxBy(x => x.DtCreated).Id : Guid.Empty;
            }).ReverseMap();
        
        CreateMap<AddressDto, Address>().ReverseMap();
    }
}
