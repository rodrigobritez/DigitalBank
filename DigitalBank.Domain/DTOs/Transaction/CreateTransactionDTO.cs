using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Domain.DTOs;

public class CreateTransactionDTO
{
  public decimal Amount { get; set; }
  public ETransactionType Type { get; set; }
}