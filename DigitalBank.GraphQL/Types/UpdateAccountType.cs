using DigitalBank.Domain.Entities;
using GraphQL.Types;

namespace DigitalBank.GraphQL.Types;

public class UpdateAccountType : InputObjectGraphType<Account>
{
  public UpdateAccountType()
  {
    Field(x => x.Name, true).Description("The name of account");
    Field(x => x.AccountNumber).Description("The number of account");
    Field(x => x.DocumentNumber, true).Description("The document number of account");
  }
}