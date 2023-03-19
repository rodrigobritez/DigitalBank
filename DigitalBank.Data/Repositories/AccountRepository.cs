using System.Linq.Expressions;
using DigitalBank.Data.Context;
using DigitalBank.Domain.Entities;
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
    var set = _context.Set<Account>().AsQueryable<Account>();
    
    foreach (var includeProperty in includes)
      set = set.Include(includeProperty);

    if (filter != null)
      set = set.Where(filter);

    return (await set.AsNoTracking().FirstOrDefaultAsync(cancellationToken))!;
  }
  
  public async Task UpdateAsync(Account account, CancellationToken cancellationToken)
  {
    var now = DateTime.Now.ToUniversalTime();
    
    account.UpdatedAt = now;

    await Task.Run( () => _context.Set<Account>().Update(account), cancellationToken);
  }
}