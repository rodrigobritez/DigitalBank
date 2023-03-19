using DigitalBank.Domain.Entities;
using GraphQL.Types;

namespace DigitalBank.GraphQL.Types;

public class CreateAccountType : InputObjectGraphType<Account>
{
  public CreateAccountType()
  {
    Field(x => x.Name).Description("The name of account");
    Field(x => x.AccountNumber).Description("The number of account");
    Field(x => x.DocumentNumber).Description("The document number of account");
  }
}