using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;

namespace DigitalBank.Tests.Mocks.Handlers;

public class FakeTransactionHandler : ITransactionHandler
{
  private Account _account = new("Rodrigo", "10358565901", 12345);
  
  public Task<ICommandResult<Transaction>> HandleAsync(CreateTransactionCommand command)
  {
    if (command.Transaction.Account!.AccountNumber != _account.AccountNumber)
      return Task.FromResult<ICommandResult<Transaction>>(new CommandResult<Transaction>(
        ECommandResultStatus.ERROR,
        "Account not found",
        null!,
        "ACCOUNT_NOT_FOUND"));
    
    _account.Deposit(command.Transaction.Amount);
    command.Transaction.Account = _account;
    return Task.FromResult<ICommandResult<Transaction>>(new CommandResult<Transaction>(
      ECommandResultStatus.SUCCESS,
      "Success Created",
      command.Transaction));
  }
}