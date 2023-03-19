using DigitalBank.Data.Context;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Data.Repositories;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
  private readonly DbContext _context;
  
  public TransactionRepository(DataContext context) : base(context)
  {
    _context = context;
  }
  
  public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken)
  {
    var now = DateTime.Now.ToUniversalTime();
    
    transaction.CreatedAt = now;
    transaction.UpdatedAt = now;
    transaction.DeletedAt = null;
    transaction.Deleted = false;


    await _context.Set<Transaction>().AddAsync(transaction, cancellationToken);
  }
}