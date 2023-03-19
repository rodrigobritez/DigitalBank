using DigitalBank.Domain.Entities;
using GraphQL.Types;

namespace DigitalBank.GraphQL.Types;

public class AccountType : ObjectGraphType<Account>
{
  public AccountType()
  {
    Field(x => x.Name).Description("The name of account");
    Field(x => x.Balance).Description("The balance of account");
    Field(x => x.AccountNumber).Description("The number of account");
    Field(x => x.DocumentNumber).Description("The document number of account");
    Field(x => x.Transactions, false,  typeof(NonNullGraphType<ListGraphType<TransactionType>>)).
      Description("The transactions of account");
  }
}