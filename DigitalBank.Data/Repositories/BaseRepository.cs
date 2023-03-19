using DigitalBank.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Data.Repositories;

public class BaseRepository : IBaseRepository
{
  private readonly DbContext _context;

  public BaseRepository(DbContext context)
  {
    _context = context;
  }
  
  public async Task Commit(CancellationToken cancellationToken)
  {
    Exception shouldThrow = null!;

    try
    {
      await _context.SaveChangesAsync(cancellationToken);
    }
    catch (Exception e)
    {
      await _context.DisposeAsync();
      shouldThrow = e;
    }

    if (shouldThrow != null) throw shouldThrow;
  }
}