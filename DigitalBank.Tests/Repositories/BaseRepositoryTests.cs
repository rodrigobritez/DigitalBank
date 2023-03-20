using DigitalBank.Data.Context;
using DigitalBank.Data.Repositories;
using DigitalBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Tests.Repositories;

[TestClass]
public class BaseRepositoryTests
{
  private DataContext _context;
  private BaseRepository _repository;

  [TestInitialize]
  public void TestInitialize()
  {
    _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _repository = new BaseRepository(_context);
  }

  [TestMethod]
  public async Task Commit_Should_Save_Changes_To_Database()
  {
    var account = new Account("John Doe", "12345678900", 12345);
    await _context.Set<Account>().AddAsync(account);
    
    await _repository.Commit(CancellationToken.None);
    
    var result = await _context.Set<Account>().SingleOrDefaultAsync(a => a.Id == account.Id);
    Assert.IsNotNull(result);
    Assert.AreEqual(account.Name, result.Name);
    Assert.AreEqual(account.DocumentNumber, result.DocumentNumber);
    Assert.AreEqual(account.AccountNumber, result.AccountNumber);
  }
}