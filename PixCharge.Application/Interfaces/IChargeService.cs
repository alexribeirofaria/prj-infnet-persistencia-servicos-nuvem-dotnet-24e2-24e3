using PixCharge.Application.Transactions.Dto;
using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Service.Interfaces;
public interface IChargeService
{
    ChargeDto Create(ChargeDto obj);
    List<ChargeDto> FindAll(Guid userId);
    ChargeDto FindById(Guid id);
    ChargeDto Update(ChargeDto obj);
    bool Delete(ChargeDto obj);
    Charge? CreateTransaction(Customer customer, Flat flat, string description = "");
}