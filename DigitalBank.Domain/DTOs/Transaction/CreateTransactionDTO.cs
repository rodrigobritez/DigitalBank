using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Domain.DTOs;

public class CreateTransactionDTO
{
  public CreateTransactionDTO(decimal amount, ETransactionType type)
  {
    Amount = amount;
    Type = type;
  }

  public CreateTransactionDTO()
  {
  }

  public decimal Amount { get; set; }
  public ETransactionType Type { get; set; }
}