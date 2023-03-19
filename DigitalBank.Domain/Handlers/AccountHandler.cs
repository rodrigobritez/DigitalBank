using System.Transactions;
using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.Domain.Interfaces.Repositories;
using DigitalBank.Domain.Validators;
using Validator = DigitalBank.Domain.Validators.Validator;

namespace DigitalBank.Domain.Handlers;

public class AccountHandler : BaseHandler, IAccountHandler
{
  private readonly IAccountRepository _accountRepository;

  public AccountHandler(IAccountRepository accountRepository)
  {
    _accountRepository = accountRepository;
  }

  public async Task<ICommandResult<Account>> HandleAsync(GetByIdCommand command)
  {
    try
    {
      var account = await _accountRepository.FindAsync(
        x => x.Id == command.Id,
        new[] { "Transactions" },
        command.CancellationToken);
      
      return new CommandResult<Account>(
        ECommandResultStatus.SUCCESS,
        "Successfully Retrieved",
        account);
    }
    catch (Exception e)
    {
      return HandleErrors(e).ToCommandResult<Account>();
    }
  }

  public async Task<ICommandResult<Account>> HandleAsync(CreateAccountCommand command)
  {
    try
    {
      var validationResult = await Validator.ValidateAsync<AccountValidator, Account>(
        command.Account, BaseEntityValidations.CREATE, command.CancellationToken);

      if (!validationResult.IsValid)
        return new CommandResult<Account>(
          ECommandResultStatus.ALERT,
          validationResult.ToString(),
          command.Account,
          "VALIDATION_ERROR"
        );
      
      using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
      {
        var account = await _accountRepository.FindAsync(
          x => x.AccountNumber == command.Account.AccountNumber,
          new[] { "Transactions" },
          command.CancellationToken);
        
        await _accountRepository.AddAsync(command.Account, command.CancellationToken);
        await _accountRepository.Commit(command.CancellationToken);
        scope.Complete();
      }
      
      return new CommandResult<Account>(
        ECommandResultStatus.SUCCESS,
        "Successfully Created",
        command.Account);
    }
    catch (Exception e)
    {
      return HandleErrors(e).ToCommandResult<Account>();
    }
  }
  
  public async Task<ICommandResult<Account>> HandleAsync(UpdateAccountCommand command)
  {
    try
    {
      var validationResult = await Validator.ValidateAsync<AccountValidator, Account>(
        command.Account, BaseEntityValidations.UPDATE, command.CancellationToken);

      if (!validationResult.IsValid)
        return new CommandResult<Account>(
          ECommandResultStatus.ALERT,
          validationResult.ToString(),
          command.Account,
          "VALIDATION_ERROR"
        );
      
      var account = await _accountRepository.FindAsync(
        x => x.AccountNumber == command.Account.AccountNumber,
        null!,
        command.CancellationToken);

      if(account == null!)
        return new CommandResult<Account>(
          ECommandResultStatus.ALERT,
          "Account not found",
          null!,
          "ACCOUNT_NOT_FOUND"
        );
      
      command.Account.Id = account.Id;
      command.Account.CreatedAt = account.CreatedAt;
      command.Account.Name ??= account.Name;
      command.Account.DocumentNumber ??= account.DocumentNumber;
      
      await _accountRepository.UpdateAsync(command.Account, command.CancellationToken);
      await _accountRepository.Commit(command.CancellationToken);
      
      return new CommandResult<Account>(
        ECommandResultStatus.SUCCESS,
        "Successfully Updated",
        command.Account);
    }
    catch (Exception e)
    {
      return HandleErrors(e).ToCommandResult<Account>();
    }
  }

  public async Task<ICommandResult<Account>> HandleAsync(GetAccountByNumberCommand command)
  {
    try
    {
      var account = await _accountRepository.FindAsync(
        x => x.AccountNumber == command.AccountNumber,
        null!,
        command.CancellationToken);
      
      return new CommandResult<Account>(
        ECommandResultStatus.SUCCESS,
        "Successfully Retrieved",
        account);
    }
    catch (Exception e)
    {
      return HandleErrors(e).ToCommandResult<Account>();
    }
  }
}