using GraphQL.Types;

namespace DigitalBank.GraphQL.Mutations;

public class RootMutation : ObjectGraphType
{
  public RootMutation()
  {
    Name = "Mutations";
    Description = "Mutations for the Digital Bank GraphQL API";

    AddMutation<AccountMutation>();
    AddMutation<TransactionMutation>();
  }

  private void AddMutation<TMutation>()
    where TMutation : ObjectGraphType, new()
  {
    var name = typeof(TMutation).Name.Replace("Mutation", "");

    Field<TMutation>(name, resolve: _ => new TMutation());
  }
}