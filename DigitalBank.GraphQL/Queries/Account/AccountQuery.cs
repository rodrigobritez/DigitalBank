using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalBank.GraphQL.Queries;

public class AccountQuery : ObjectGraphType
{
  public AccountQuery()
  {
    Field<CommandResultType<Account, AccountType>>()
      .Name("GetAccountById")
      .Argument<NonNullGraphType<GuidGraphType>>(
        "id", "The id of account")
      .ResolveAsync(async context =>
      {
        var accountHandler =
          context.RequestServices!.GetRequiredService<IAccountHandler>();
        var accountDynamic = context.GetArgument<Guid>("id");
        var getAccountByIdCommand = new GetByIdCommand(accountDynamic, context.CancellationToken);

        var response = await accountHandler.HandleAsync(getAccountByIdCommand);

        if (response.Status != ECommandResultStatus.ERROR)
          return response;

        throw new ExecutionError(response.Message, new Dictionary<string, string>
        {
          { "errorCode", response.ErrorCode},
          { "status", response.Status.ToString() }
        });
      });
    
    Field<CommandResultType<Account, AccountType>>()
      .Name("GetAccountByAccountNumber")
      .Argument<NonNullGraphType<IntGraphType>>(
        "accountNumber", "The id of account")
      .ResolveAsync(async context =>
      {
        var accountHandler =
          context.RequestServices!.GetRequiredService<IAccountHandler>();
        var accountNumberDynamic = context.GetArgument<int>("accountNumber");
        var getAccountByNumberCommand = new GetAccountByNumberCommand(accountNumberDynamic, context.CancellationToken);

        var response = await accountHandler.HandleAsync(getAccountByNumberCommand);

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