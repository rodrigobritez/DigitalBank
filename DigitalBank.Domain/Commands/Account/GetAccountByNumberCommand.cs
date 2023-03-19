using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Commands;

public class GetAccountByNumberCommand : ICommand
{
  public GetAccountByNumberCommand(int accountNumber, CancellationToken cancellationToken)
  {
    AccountNumber = accountNumber;
    CancellationToken = cancellationToken;
  }

  public int AccountNumber { get; }
  public CancellationToken CancellationToken { get; }
}