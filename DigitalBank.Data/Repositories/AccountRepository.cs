using System.Linq.Expressions;
using DigitalBank.Data.Context;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Entities.Base;
using DigitalBank.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Data.Repositories;

public class AccountRepository : BaseRepository, IAccountRepository
{
  private readonly DbContext _context;

  public AccountRepository(DataContext context) : base(context)
  {
    _context = context;
  }

  public async Task AddAsync(Account account, CancellationToken cancellationToken)
  {
    var now = DateTime.Now.ToUniversalTime();

    account.CreatedAt = now;
    account.UpdatedAt = now;
    account.DeletedAt = null;
    account.Deleted = false;


    await _context.Set<Account>().AddAsync(account, cancellationToken);
  }

  public async Task<Account> FindAsync(
    Expression<Func<Account, bool>> filter,
    IEnumerable<string> includeProperties,
    CancellationToken cancellationToken)
  {
    var includes = includeProperties ?? Array.Empty<string>();
    var query = _context.Set<Account>().AsQueryable<Account>();

    foreach (var includeProperty in includes)
      query = query.Include(includeProperty);

    if (filter != null)
      query = query.Where(filter);

    return (await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken))!;
  }

  public async Task UpdateAsync(Account account, CancellationToken cancellationToken)
  {
    var now = DateTime.Now.ToUniversalTime();

    account.UpdatedAt = now;

    await Task.Run(() => _context.Set<Account>().Update(account), cancellationToken);
  }

  public async Task SoftDeleteAsync(Account account, CancellationToken cancellationToken)
  {
    var accountToDelete = await ValidateExistenceAsync(account, cancellationToken);

    var now = DateTime.Now.ToUniversalTime();

    accountToDelete.DeletedAt = now;
    accountToDelete.Deleted = true;

    if (accountToDelete.Transactions != null)
      foreach (var transaction in accountToDelete.Transactions)
      {
        transaction.Deleted = true;
        transaction.DeletedAt = now;
      }
    await UpdateAsync(accountToDelete, cancellationToken);
  }

  private async Task<Account> ValidateExistenceAsync(Account account, CancellationToken cancellationToken)
  {
    var query = _context.Set<Account>().AsQueryable<Account>();
    query = query.Include("Transactions");
    query = query.Where(x => x.AccountNumber == account.AccountNumber);
    var accountToDelete = await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    if (accountToDelete == null)
      throw new Exception($"Account Number '{account.AccountNumber}' not found");
    
    return accountToDelete;
  }
  
  public Task DetachEntity(Account account)
  {
    _context.Entry(account).State = EntityState.Detached;
    return Task.CompletedTask;
  }
}