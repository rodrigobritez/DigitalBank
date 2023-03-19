using DigitalBank.Domain.Handlers;
using DigitalBank.Domain.Interfaces.Handlers;

namespace DigitalBank.API.Extensions;

public static class ScopedHandlersExtensions
{
  public static void AddCustomHandlers(this IServiceCollection services)
  {
    services.AddTransient<IAccountHandler, AccountHandler>();
    services.AddTransient<ITransactionHandler, TransactionHandler>();
  }
}