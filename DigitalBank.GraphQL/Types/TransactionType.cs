using DigitalBank.Domain.Entities;
using GraphQL.Types;

namespace DigitalBank.GraphQL.Types;

public class TransactionType : ObjectGraphType<Transaction>
{
  public TransactionType()
  {
    Field(x => x.Amount).Description("The amount of transaction");
    Field(x => x.Type).Description("The type of transaction");
    Field(x => x.AccountId).Description("The account id of transaction");
  }
}