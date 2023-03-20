using System.Transactions;
using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.Domain.Interfaces.Repositories;
using DigitalBank.Domain.Validators;
using DigitalBank.Shared.Constants.Enums;
using Transaction = DigitalBank.Domain.Entities.Transaction;

namespace DigitalBank.Domain.Handlers;

public class TransactionHandler : BaseHandler, ITransactionHandler
{
  private readonly ITransactionRepository _transactionRepository;
  private readonly IAccountRepository _accountRepository;

  public TransactionHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
  {
    _transactionRepository = transactionRepository;
    _accountRepository = accountRepository;
  }
  
  public async Task<ICommandResult<Transaction>> HandleAsync(CreateTransactionCommand command)
  {
    try
    {
      var validationResult = await Validator.ValidateAsync<TransactionValidator, Transaction>(
        command.Transaction, BaseEntityValidations.CREATE, command.CancellationToken);

      if (!validationResult.IsValid)
        return new CommandResult<Transaction>(
          ECommandResultStatus.ERROR,
          validationResult.ToString(),
          command.Transaction,
          "VALIDATION_ERROR"
        );
      
      var account = await _accountRepository.FindAsync(
        x => x.AccountNumber == command.Transaction.Account!.AccountNumber,
        null!,
        command.CancellationToken);
      
      if(account == null!)
        return new CommandResult<Transaction>(
          ECommandResultStatus.ERROR,
          "Account not found",
          null!,
          "ACCOUNT_NOT_FOUND"
        );
      
      using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
      {

        switch (command.Transaction.Type)
        {
          case ETransactionType.WITHDRAW when account.Balance < command.Transaction.Amount:
            return new CommandResult<Transaction>(
              ECommandResultStatus.ERROR,
              "Insufficient funds",
              command.Transaction,
              "INSUFFICIENT_FUNDS");
          case ETransactionType.WITHDRAW:
            account.WithDraw(command.Transaction.Amount);
            await _accountRepository.UpdateAsync(account, command.CancellationToken);
            await _accountRepository.Commit(command.CancellationToken);
            break;
          case ETransactionType.DEPOSIT:
            account.Deposit(command.Transaction.Amount);
            await _accountRepository.UpdateAsync(account, command.CancellationToken);
            await _accountRepository.Commit(command.CancellationToken);
            break;
        }

        command.Transaction.AccountId = account.Id;
        command.Transaction.Account = null;

        await _transactionRepository.AddAsync(command.Transaction, command.CancellationToken);
        await _transactionRepository.Commit(command.CancellationToken);
        scope.Complete();
      }

      return new CommandResult<Transaction>(
        ECommandResultStatus.SUCCESS,
        "Successfully Created",
        command.Transaction);
    }
    catch (Exception e)
    {
      return HandleErrors(e).ToCommandResult<Transaction>();
    }
  }
}