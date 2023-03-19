using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Commands;

public class CreateTransactionCommand : ICommand
{
  public CreateTransactionCommand(Transaction transaction, int accountNumber, CancellationToken cancellationToken)
  {
    Transaction = transaction;
    AccountNumber = accountNumber;
    CancellationToken = cancellationToken;
  }

  public Transaction Transaction { get; }
  public int AccountNumber { get; }
  public CancellationToken CancellationToken { get; }
}