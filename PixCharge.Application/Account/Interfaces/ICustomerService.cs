using PixCharge.Application.Account.Dto;

namespace PixCharge.Application.Account.Interfaces;
public interface ICustomerService
{
    CustomerDto Create(CustomerDto obj);
    List<CustomerDto> FindAll(Guid userId);
    CustomerDto FindById(Guid id);
    CustomerDto Update(CustomerDto obj);
    bool Delete(CustomerDto obj);
}
