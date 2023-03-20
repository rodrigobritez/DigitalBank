using DigitalBank.API.Controllers;
using DigitalBank.Data.Context;
using DigitalBank.Data.Repositories;
using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Handlers;
using DigitalBank.Domain.Interfaces.Commands;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Tests.Controllers;

[TestClass]
public class AccountControllerTests
{
  private DataContext _context;
  private AccountHandler _handler;
  private AccountController _controller;
  private AccountRepository _repository;

  [TestInitialize]
  public void Setup()
  {
    _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _repository = new AccountRepository(_context);
    _handler = new AccountHandler(_repository);
    _controller = new AccountController();
  }

  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task GetByAccountNumber_ReturnsAccount()
  {
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;
    var expectedAccount = new Account("Test Account", "123456789", accountNumber);
    
    await _repository.AddAsync(expectedAccount, cancellationToken);
    await _repository.Commit(cancellationToken);
    
    var result = await _controller.GetByAccountNumber(accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    Assert.AreEqual(accountNumber, result.Result.AccountNumber);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task UpdateAccount_ReturnsAccount()
  {
    const int accountNumber = 55555;
    var accountDto = new UpdateAccountDTO { Name = "New Name", DocumentNumber = "12345678901" };
    CancellationToken cancellationToken = default;
    var expectedAccount = new Account(accountDto.Name, "555555555555", accountNumber);
    
    await _repository.AddAsync(expectedAccount, cancellationToken);
    await _repository.Commit(cancellationToken);
    await _repository.DetachEntity(expectedAccount);

    var result = await _controller.UpdateAccount(accountDto, accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    Assert.AreEqual(accountDto.DocumentNumber, result.Result.DocumentNumber);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task CreateAccount_ReturnsAccount()
  {
    const string name = "Test Name";
    const string documentNumber = "12345678901";
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;
    var expectedAccount = new Account(name, documentNumber, accountNumber);
    
    var result = await _controller.CreateAccount(name, documentNumber, accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    Assert.AreEqual(expectedAccount.AccountNumber, result.Result.AccountNumber);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task DeleteAccount_ReturnsAccount()
  {
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;
    var expectedAccount = new Account("Test Name", "555555555555", accountNumber);
    
    await _repository.AddAsync(expectedAccount, cancellationToken);
    await _repository.Commit(cancellationToken);
    await _repository.DetachEntity(expectedAccount);
    
    var result = await _controller.DeleteAccount(accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
  }
}