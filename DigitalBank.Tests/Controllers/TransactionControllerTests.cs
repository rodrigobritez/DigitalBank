using DigitalBank.API.Controllers;
using DigitalBank.Data.Context;
using DigitalBank.Data.Repositories;
using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Handlers;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Shared.Constants.Enums;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Tests.Controllers;

[TestClass]
public class TransactionControllerTests
{
  private DataContext _context;
  private TransactionHandler _handler;
  private TransactionController _controller;
  private AccountRepository _accountRepository;
  private TransactionRepository _transactionRepository;
  

  [TestInitialize]
  public void Setup()
  {
    _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _accountRepository = new AccountRepository(_context);
    _transactionRepository = new TransactionRepository(_context);
    _handler = new TransactionHandler(_transactionRepository, _accountRepository);
    _controller = new TransactionController();
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task CreateTransaction_Deposit_Success()
  {
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;
    var account = new Account("Test Name", "12345678901", accountNumber);
    var transactionDto = new CreateTransactionDTO(500, ETransactionType.DEPOSIT);
    
    await _accountRepository.AddAsync(account, cancellationToken);
    await _accountRepository.Commit(cancellationToken);
    await _accountRepository.DetachEntity(account);

    var result = await _controller.CreateAccount(transactionDto, accountNumber , _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task CreateTransaction_WithDraw_Error()
  {
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;
    var account = new Account("Test Name", "12345678901", accountNumber);
    var transactionDto = new CreateTransactionDTO(500, ETransactionType.WITHDRAW);
    
    await _accountRepository.AddAsync(account, cancellationToken);
    await _accountRepository.Commit(cancellationToken);
    await _accountRepository.DetachEntity(account);

    var result = await _controller.CreateAccount(transactionDto, accountNumber , _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
  }
}