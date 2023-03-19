using DigitalBank.Domain.Entities.Base;
using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Domain.Entities;

public class Transaction : BaseEntity
{
  public Transaction(decimal amount, ETransactionType type)
  {
    Amount = amount;
    Type = type;
  }

  public Transaction()
  {
  }

  public decimal Amount { get; set; }
  public ETransactionType Type { get; set; }
  public Guid AccountId { get; set; }
  public virtual Account? Account { get; set; }
}