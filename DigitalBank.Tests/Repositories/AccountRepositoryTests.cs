using DigitalBank.Data.Context;
using DigitalBank.Data.Repositories;
using DigitalBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Tests.Repositories;

[TestClass]
public class AccountRepositoryTests
{
    private DataContext _context;
    private AccountRepository _repository;

    [TestInitialize]
    public void TestInitialize()
    {
        _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);
        _repository = new AccountRepository(_context);
    }

    [TestMethod]
    [TestCategory("Data-Repository")]
    public async Task AddAsync_Should_Add_Account_To_Database()
    {

        var account = new Account("John Doe", "12345678900", 12345);

        await _repository.AddAsync(account, CancellationToken.None);
        await _repository.Commit(CancellationToken.None);

        var result = await _context.Set<Account>().SingleOrDefaultAsync(a => a.Id == account.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual(account.Name, result.Name);
        Assert.AreEqual(account.DocumentNumber, result.DocumentNumber);
        Assert.AreEqual(account.AccountNumber, result.AccountNumber);
    }

    [TestMethod]
    [TestCategory("Data-Repository")]
    public async Task FindAsync_Should_Return_Account_From_Database()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        await _context.Set<Account>().AddAsync(account);
        await _context.SaveChangesAsync();
        
        var result = await _repository.FindAsync(a => a.Id == account.Id, null, CancellationToken.None);
        
        Assert.IsNotNull(result);
        Assert.AreEqual(account.Name, result.Name);
        Assert.AreEqual(account.DocumentNumber, result.DocumentNumber);
        Assert.AreEqual(account.AccountNumber, result.AccountNumber);
    }

    [TestMethod]
    [TestCategory("Data-Repository")]
    public async Task UpdateAsync_Should_Update_Account_In_Database()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        await _context.Set<Account>().AddAsync(account);
        await _context.SaveChangesAsync();
        
        account.Name = "Jane Doe";
        await _repository.UpdateAsync(account, CancellationToken.None);
        await _repository.Commit(CancellationToken.None);
        
        var result = await _context.Set<Account>().SingleOrDefaultAsync(a => a.Id == account.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual(account.Name, result.Name);
        Assert.AreEqual(account.DocumentNumber, result.DocumentNumber);
        Assert.AreEqual(account.AccountNumber, result.AccountNumber);
    }
}