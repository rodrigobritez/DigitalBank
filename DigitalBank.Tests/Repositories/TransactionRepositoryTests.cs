using DigitalBank.Data.Context;
using DigitalBank.Data.Repositories;
using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Tests.Repositories;

[TestClass]
public class TransactionRepositoryTests
{
  private DataContext _context;
  private TransactionRepository _repository;

  [TestInitialize]
  public void TestInitialize()
  {
    _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _repository = new TransactionRepository(_context);
  }

  [TestMethod]
  [TestCategory("Data-Repository")]
  public async Task AddAsync_Should_Add_Transaction_To_Database()
  {
    var account = new Account("John Doe", "12345678900", 12345);
    var transaction = new Transaction(100, ETransactionType.DEPOSIT)
    {
      AccountId = account.Id,
      Account = account
    };
    await _context.Set<Account>().AddAsync(account);
    await _context.SaveChangesAsync();
    
    await _repository.AddAsync(transaction, CancellationToken.None);
    await _repository.Commit(CancellationToken.None);
    
    var result = await _context.Set<Transaction>().SingleOrDefaultAsync(t => t.Id == transaction.Id);
    Assert.IsNotNull(result);
    Assert.AreEqual(transaction.Amount, result.Amount);
    Assert.AreEqual(transaction.Type, result.Type);
    Assert.AreEqual(transaction.AccountId, result.AccountId);
    Assert.IsNotNull(result.Account);
    Assert.AreEqual(account.Name, result.Account.Name);
    Assert.AreEqual(account.DocumentNumber, result.Account.DocumentNumber);
    Assert.AreEqual(account.AccountNumber, result.Account.AccountNumber);
  }
}