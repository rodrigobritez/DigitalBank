using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalBank.GraphQL.Mutations;

public class AccountMutation : ObjectGraphType
{
  public AccountMutation()
  {
    Field<CommandResultType<Account, AccountType>>()
      .Name("CreateAccount")
      .Argument<NonNullGraphType<CreateAccountType>>(
        "account", "create account")
      .ResolveAsync(async context =>
      {
        var accountHandler =
          context.RequestServices!.GetRequiredService<IAccountHandler>();
        var accountDynamic = context.GetArgument<Account>("account");
        var createAccountCommand = new CreateAccountCommand(accountDynamic, context.CancellationToken);

        var response = await accountHandler.HandleAsync(createAccountCommand);

        if (response.Status != ECommandResultStatus.ERROR)
          return response;

        throw new ExecutionError(response.Message, new Dictionary<string, string>
        {
          { "errorCode", response.ErrorCode},
          { "status", response.Status.ToString() }
        });
      });
    
    Field<CommandResultType<Account, AccountType>>()
      .Name("UpdateAccount")
      .Argument<NonNullGraphType<UpdateAccountType>>(
        "account", "update account")
      .ResolveAsync(async context =>
      {
        var accountHandler =
          context.RequestServices!.GetRequiredService<IAccountHandler>();
        var accountDynamic = context.GetArgument<Account>("account");
        var updateAccountCommand = new UpdateAccountCommand(accountDynamic, context.CancellationToken);

        var response = await accountHandler.HandleAsync(updateAccountCommand);

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