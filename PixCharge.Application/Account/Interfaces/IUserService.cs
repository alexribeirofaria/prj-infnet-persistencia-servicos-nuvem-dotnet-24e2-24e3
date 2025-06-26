using PixCharge.Application.Account.Dto;

namespace PixCharge.Application.Account.Interfaces;
public interface IUserService
{    AuthenticationDto Authentication(LoginDto dto);
}
