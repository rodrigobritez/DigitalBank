using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;

namespace DigitalBank.Tests.Mocks.Handlers;

public class FakeAccountHandler : IAccountHandler
{
  private Account _account = new("Rodrigo", "10358565901", 12345);
  public Task<ICommandResult<Account>> HandleAsync(GetByIdCommand command)
  {
    _account.Id = command.Id;
    return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
      ECommandResultStatus.SUCCESS,
      "Success Retrieving",
      _account));
  }

  public Task<ICommandResult<Account>> HandleAsync(CreateAccountCommand command)
  {
    return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
      ECommandResultStatus.SUCCESS,
      "Success Created",
      command.Account));
  }

  public Task<ICommandResult<Account>> HandleAsync(UpdateAccountCommand command)
  {
    if (command.Account.AccountNumber != _account.AccountNumber)
      return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
        ECommandResultStatus.ERROR,
        "Account not found",
        null!,
        "ACCOUNT_NOT_FOUND"));
    
    _account.Name = command.Account.Name;
    _account.DocumentNumber = command.Account.DocumentNumber;
    return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
      ECommandResultStatus.SUCCESS,
      "Success Updated",
      _account));

  }

  public Task<ICommandResult<Account>> HandleAsync(GetAccountByNumberCommand command)
  {
    if (command.AccountNumber == _account.AccountNumber)
    {
      return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
        ECommandResultStatus.SUCCESS,
        "Success Retrieving",
        _account));
    }
    
    return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
      ECommandResultStatus.ERROR,
      "Account not found",
      null!,
      "ACCOUNT_NOT_FOUND"));
  }

  public Task<ICommandResult<Account>> HandleAsync(DeleteAccountCommand command)
  {
    if(command.Account.AccountNumber == _account.AccountNumber)
      return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
        ECommandResultStatus.SUCCESS,
        "Success Deleted",
        _account));
    
    return Task.FromResult<ICommandResult<Account>>(new CommandResult<Account>(
      ECommandResultStatus.ERROR,
      "Account not found",
      null!,
      "ACCOUNT_NOT_FOUND"));
  }
}