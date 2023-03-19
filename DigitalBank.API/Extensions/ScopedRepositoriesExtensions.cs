using DigitalBank.Data.Repositories;
using DigitalBank.Domain.Interfaces.Repositories;

namespace DigitalBank.API.Extensions;

public static class ScopedRepositoriesExtensions
{
  public static void AddCustomRepositories(this IServiceCollection services)
  {
    services.AddTransient<IAccountRepository, AccountRepository>();
    services.AddTransient<ITransactionRepository, TransactionRepository>();
  }

}