using DigitalBank.GraphQL.Mutations;
using DigitalBank.GraphQL.Queries;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalBank.GraphQL;

public class GraphQLMainSchema : Schema
{
  public GraphQLMainSchema(IServiceProvider provider) : base(provider)
  {
    Query = provider.GetRequiredService<RootQuery>();
    Mutation = provider.GetRequiredService<RootMutation>();
  }
}