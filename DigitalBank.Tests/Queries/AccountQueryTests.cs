using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.GraphQL;
using DigitalBank.GraphQL.Mutations;
using DigitalBank.GraphQL.Queries;
using DigitalBank.GraphQL.Types;
using DigitalBank.Tests.Mocks.Handlers;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalBank.Tests.Queries;

[TestClass]
public class AccountQueryTests
{
  private IServiceProvider _serviceProvider;
  
  [TestInitialize]
  public void TestInitialize()
  {
    _serviceProvider = new ServiceCollection()
      .AddSingleton<RootQuery>()
      .AddSingleton<RootMutation>()
      .AddSingleton<AccountQuery>()
      .AddSingleton<AccountMutation>()
      .AddSingleton<TransactionMutation>()
      .AddSingleton<CreateAccountType>()  
      .AddSingleton<DeleteAccountType>()
      .AddSingleton<UpdateAccountType>()
      .AddSingleton<CreateTransactionType>()
      .AddSingleton<AccountTransactionType>()
      .AddSingleton<AccountType>()
      .AddSingleton<TransactionType>()
      .AddTransient<IAccountHandler, FakeAccountHandler>()
      .AddSingleton<CommandResultType<Account, AccountType>>()
      .AddSingleton<CommandResultType<Transaction, TransactionType>>()
      .AddSingleton(typeof(EnumerationGraphType<>))
      .BuildServiceProvider();
  }
  
  [TestMethod]
  [TestCategory("GraphQL-Query")]
  public async Task GetAccountByIdQuery_Returns_Success()
  {
    var id = Guid.NewGuid();
    var query = $"query {{ account {{ getAccountById(id: \"{id}\") {{ status message errorCode result {{ name documentNumber accountNumber balance }} }} }} }}";

    var schema = new GraphQLMainSchema(_serviceProvider);
    
    var result = await new DocumentExecuter().ExecuteAsync(options =>
    {
      options.RequestServices= _serviceProvider;
      options.Schema = schema;
      options.Query = query;
    });
    
    Assert.IsTrue(result.Executed);
    Assert.IsNull(result.Errors);
  }

  [TestMethod]
  [TestCategory("GraphQL-Query")]
  public async Task GetAccountByAccountNumberQuery_Returns_Success()
  {
    const int accountNumber = 12345;

    var query = $"query {{ __typename account {{ getAccountByAccountNumber(accountNumber: {accountNumber}) {{ status message errorCode result {{ name documentNumber accountNumber }} }} }} }}";

    var schema = new GraphQLMainSchema(_serviceProvider);
    
    var result = await new DocumentExecuter().ExecuteAsync(options =>
    {
      options.RequestServices= _serviceProvider;
      options.Schema = schema;
      options.Query = query;
    });
    
    Assert.IsTrue(result.Executed);
    Assert.IsNull(result.Errors);
  }
}