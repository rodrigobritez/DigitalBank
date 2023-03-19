using GraphQL.Types;

namespace DigitalBank.GraphQL.Queries;

public class RootQuery : ObjectGraphType
{
  public RootQuery()
  {
    Name = "Queries";
    Description = "Queries for the Digital Bank GraphQL API";

    AddQuery<AccountQuery>();
  }
  
  private void AddQuery<TQuery>()
    where TQuery : ObjectGraphType, new()
  {
    var name = typeof(TQuery).Name.Replace("Query", "");

    Field<TQuery>(name, resolve: _ => new TQuery());
  }
}