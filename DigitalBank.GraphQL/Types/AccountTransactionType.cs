using DigitalBank.Domain.Entities;
using GraphQL.Types;

namespace DigitalBank.GraphQL.Types;

public class AccountTransactionType : InputObjectGraphType<Account>
{
  public AccountTransactionType()
  {
    Field(x => x.AccountNumber).Description("The number of account");
  }
}