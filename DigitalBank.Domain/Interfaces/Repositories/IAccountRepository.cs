using System.Linq.Expressions;
using DigitalBank.Domain.Entities;

namespace DigitalBank.Domain.Interfaces.Repositories;

public interface IAccountRepository : IBaseRepository
{
  public Task AddAsync(Account account, CancellationToken cancellationToken);

  public Task<Account> FindAsync(
    Expression<Func<Account, bool>> filter,
    IEnumerable<string> includeProperties,
    CancellationToken cancellationToken);

  public Task UpdateAsync(Account account, CancellationToken cancellationToken);
  public Task SoftDeleteAsync(Account account, CancellationToken cancellationToken);
  public Task DetachEntity(Account account);
}