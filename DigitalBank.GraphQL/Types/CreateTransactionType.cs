using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;
using GraphQL.Types;

namespace DigitalBank.GraphQL.Types;

public class CreateTransactionType : InputObjectGraphType<Transaction>
{
  public CreateTransactionType()
  {
    Field(x => x.Amount).Description("The amount of transaction");
    Field(x => x.Type, false, typeof(NonNullGraphType<EnumerationGraphType<ETransactionType>>))
      .Description("The type of transaction");
    Field(x => x.Account, false, typeof(NonNullGraphType<AccountTransactionType>))
      .Description("The account number of transaction");
  }
}