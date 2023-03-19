using DigitalBank.Domain.Entities;

namespace DigitalBank.Domain.Interfaces.Repositories;

public interface ITransactionRepository : IBaseRepository
{
  public Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
}