using DigitalBank.Domain.Entities;
using GraphQL.Types;

namespace DigitalBank.GraphQL.Types;

public class DeleteAccountType : InputObjectGraphType<Account>
{
  public DeleteAccountType()
  {
    Field(x => x.AccountNumber).Description("The number of account");
  }
}