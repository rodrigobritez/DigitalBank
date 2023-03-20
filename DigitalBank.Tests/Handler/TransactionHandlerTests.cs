using DigitalBank.Data.Context;
using DigitalBank.Data.Repositories;
using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Handlers;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.Domain.Interfaces.Repositories;
using DigitalBank.Shared.Constants.Enums;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Tests.Handler;

[TestClass]
public class TransactionHandlerTests
{
    private DataContext _context;
    private ITransactionHandler _handler;
    private IAccountRepository _accountRepository;
    private ITransactionRepository _transactionRepository;
  

    [TestInitialize]
    public void Setup()
    {
        _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);
        _accountRepository = new AccountRepository(_context);
        _transactionRepository = new TransactionRepository(_context);
        _handler = new TransactionHandler(_transactionRepository, _accountRepository);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task CreateTransaction_Should_Return_Success()
    {
        const int accountNumber = 55555;
        CancellationToken cancellationToken = default;
        var account = new Account("Test Name", "12345678901", accountNumber);
        var transaction = new Transaction(500, ETransactionType.DEPOSIT)
        {
            Account = account
        };
        var command = new CreateTransactionCommand(transaction, accountNumber, cancellationToken);
    
        await _accountRepository.AddAsync(account, cancellationToken);
        await _accountRepository.Commit(cancellationToken);
        await _accountRepository.DetachEntity(account);

        var result = await _handler.HandleAsync(command);
    
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    }
}