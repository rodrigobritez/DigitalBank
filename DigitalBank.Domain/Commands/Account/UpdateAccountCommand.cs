using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Commands;

public class UpdateAccountCommand : ICommand
{
  public UpdateAccountCommand(Account account, CancellationToken cancellationToken)
  {
    Account = account;
    CancellationToken = cancellationToken;
  }
  
  public Account Account { get; }
  public CancellationToken CancellationToken { get; }
  
}