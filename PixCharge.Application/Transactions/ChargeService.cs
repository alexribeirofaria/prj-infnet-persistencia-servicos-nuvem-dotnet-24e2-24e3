using PixCharge.Integration.Adapter;
using PixCharge.Integration.Adapter.Plataform.OpenPix;
using PixCharge.Service.Interfaces;
using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Transactions.Aggregates;
using PixCharge.Application.Transactions.Dto;
using AutoMapper;
using PixCharge.Repository;
using PixCharge.Domain.Transactions.ValueObject;

namespace PixCharge.Application.Transactions;
public class ChargeService : ServiceBase<ChargeDto, Charge>, IService<ChargeDto>, IChargeService
{
    private const int INTERVAL_TRANSACTON_PIX = -4;
    private readonly IRepository<Customer> _customerRepository;
    public ChargeService(IMapper mapper,
        IRepository<Charge> chargeRepository,
        IRepository<Customer> customerRepository) : base(mapper, chargeRepository)
    {
        _customerRepository = customerRepository;        
    }

    public override ChargeDto Create(ChargeDto dto)
    {
        try
        {
            var customer = this._customerRepository.GetById(dto.CustomerId);
            var flat = customer.Flats.FirstOrDefault(f => f.Id == dto.FlatId);
            var charge = CreateTransaction(customer, flat ?? throw new ArgumentException("Plano inválido!"));
            return this.Mapper.Map<ChargeDto>(charge);
        }
        catch
        {
            throw;
        }
    }

    public override bool Delete(ChargeDto obj)
    {
        throw new NotImplementedException();
    }

    public override List<ChargeDto> FindAll(Guid userId)
    {
        throw new NotImplementedException();
    }

    public override ChargeDto FindById(Guid id)
    {
        var charge = this.Repository.GetById(id);
        if (IsChargeApporve(charge.CorrelationId))
        {
            charge.Status = "Completed";
            charge.PIX.Status = Status.Approved;
            this.Repository.Update(ref charge);
            return this.Mapper.Map<ChargeDto>(charge);
        }

        return this.Mapper.Map<ChargeDto>(charge);
    }

    public override ChargeDto Update(ChargeDto obj)
    {
        throw new NotImplementedException();
    }
    public Charge? CreateTransaction(Customer customer, Flat flat, string description = "")
    {
        Charge? charge = null;
        try
        {

            Transaction transaction = new Transaction
            {
                Customer = customer,
                Value = flat.Value,
                Description = description,
                DtTransaction = DateTime.Now,
            };
            
            // Verificãção de Existência de uma Transação PIX já Criada com o mesmo Valor dentro do Intervalo Minimo
             transaction = ValidateTransaction(transaction, customer) ?? transaction;
            
            if (transaction == null ||  transaction.Id.Equals(Guid.Empty))
            {
                transaction.Id = Guid.NewGuid();
                IPix chargePix = new OpenPix();
                charge = chargePix.CreateCharge(flat.Value, transaction?.Id.ToString() ?? Guid.NewGuid().ToString());

                charge.CorrelationId = transaction.Id;
                charge.Customer = customer;
                charge.Flat = flat;

                var pix = new PIX(customer);
                pix.CorrelationId = charge.CorrelationId;
                pix.Description = $"Cobrança PIX {flat.Description}";
                pix.Status = Status.Pending;
                pix.QrCode.Url = charge.QrCodeImage;
                pix.QrCode.BrCode = charge.BrCode;
                pix.Value = charge.Value.Value;
                pix.Transactions.Add(transaction);
                pix.Date = DateTime.Now;
                charge.PIX = pix;
                this.Repository.Save(ref charge);
            }

            return this.Repository.Find(c => c.CorrelationId.Equals(transaction.Id)).First();
        }
        catch
        {
            throw new ArgumentException("Erro ao criar cobrança!");
        }
    }

    private Transaction? ValidateTransaction(Transaction transaction, Customer customer)
    {
        if (transaction.Value <= 0)
            throw new ArgumentException("O valor a ser cobrado não pode ser menor ou igual a 0!");

        var lastTransactions = customer.Transactions.Where(t => t.DtTransaction > DateTime.Now.AddMinutes(INTERVAL_TRANSACTON_PIX) && t.Value.Equals(transaction.Value));
        if (lastTransactions?.Count() >= 1)
            return lastTransactions.Last();

        return null;
    }

    private bool IsChargeApporve(Guid correlationId)
    {
        IPix chargePix = new OpenPix();
        return chargePix.IsChargeApporve(correlationId);        
    }
}