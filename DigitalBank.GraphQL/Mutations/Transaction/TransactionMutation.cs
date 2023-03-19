using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalBank.GraphQL.Mutations;

public class TransactionMutation : ObjectGraphType
{
  public TransactionMutation()
  {
    Field<CommandResultType<Transaction, TransactionType>>()
      .Name("CreateTransaction")
      .Argument<NonNullGraphType<CreateTransactionType>>(
        "transaction", "create transaction")
      .ResolveAsync(async context =>
      {
        var transactionHandler =
          context.RequestServices!.GetRequiredService<ITransactionHandler>();
        var transactionDynamic = context.GetArgument<Transaction>("transaction");
        var createTransactionCommand = new CreateTransactionCommand(
          transactionDynamic, transactionDynamic.Account!.AccountNumber, context.CancellationToken);

        var response = await transactionHandler.HandleAsync(createTransactionCommand);

        if (response.Status != ECommandResultStatus.ERROR)
          return response;

        throw new ExecutionError(response.Message, new Dictionary<string, string>
        {
          { "errorCode", response.ErrorCode},
          { "status", response.Status.ToString() }
        });
      });
  }
}