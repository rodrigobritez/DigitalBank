﻿using DigitalBank.Data.Context;
using DigitalBank.Data.Repositories;
using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Handlers;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigitalBank.Tests.Handler;

[TestClass]
public class AccountHandlerTests
{
    private DataContext _context;
    private IAccountRepository _repository;
    private IAccountHandler _handler;
    

    [TestInitialize]
    public void TestInitialize()
    {
        _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);
        _repository = new AccountRepository(_context);
        _handler = new AccountHandler(_repository);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task CreateAccount_Should_Return_Success()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        CancellationToken cancellationToken = default;
        var command = new CreateAccountCommand(account, cancellationToken);
        var result = await _handler.HandleAsync(command);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task CreateAccount_Should_Return_ValidationError()
    {
        var account = new Account("John Doe", "123456789005555", 12345);
        CancellationToken cancellationToken = default;
        var command = new CreateAccountCommand(account, cancellationToken);
        var result = await _handler.HandleAsync(command);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
    }

    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task UpdateAccount_Should_Return_Success()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        var accountToUpdate = new Account("Rodrigo", "12345678900", 12345);
        CancellationToken cancellationToken = default;
        var command = new UpdateAccountCommand(accountToUpdate, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
        Assert.AreNotEqual(result.Result.Name, account.Name);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task UpdateAccount_Should_Return_NotFound_Error()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        var accountToUpdate = new Account("Rodrigo", "12345678900", 55555);
        CancellationToken cancellationToken = default;
        var command = new UpdateAccountCommand(accountToUpdate, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task UpdateAccount_Should_Return_Validation_Error()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        var accountToUpdate = new Account("Rodrigo", "12345678900", 55555555);
        CancellationToken cancellationToken = default;
        var command = new UpdateAccountCommand(accountToUpdate, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task DeleteAccount_Should_Return_Success()
    {
        const int accountNumber = 12345;
        var account = new Account("John Doe", "12345678900", accountNumber);
        var accountToDelete = new Account("", "", accountNumber);
        CancellationToken cancellationToken = default;
        var command = new DeleteAccountCommand(accountToDelete, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task DeleteAccount_Should_Return_NotFound_Error()
    {
        const int accountNumber = 12345;
        var account = new Account("John Doe", "12345678900", accountNumber);
        var accountToDelete = new Account("", "", 55555);
        CancellationToken cancellationToken = default;
        var command = new DeleteAccountCommand(accountToDelete, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task GetByIdAccount_Should_Return_Success()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        CancellationToken cancellationToken = default;
        var command = new GetByIdCommand(account.Id, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
        Assert.AreEqual(account.AccountNumber, result.Result.AccountNumber);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task GetByIdAccount_Should_Return_Error()
    {
        var account = new Account("John Doe", "12345678900", 12345);
        CancellationToken cancellationToken = default;
        var command = new GetByIdCommand(Guid.NewGuid(), cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task GetByAccountNumberAccount_Should_Return_Success()
    {
        const int accountNumber = 12345;
        var account = new Account("John Doe", "12345678900", accountNumber);
        CancellationToken cancellationToken = default;
        var command = new GetAccountByNumberCommand(accountNumber, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
        Assert.AreEqual(account.AccountNumber, result.Result.AccountNumber);
        Assert.AreEqual(account.DocumentNumber, result.Result.DocumentNumber);
    }
    
    [TestMethod]
    [TestCategory("Domain-Handler")]
    public async Task GetByAccountNumberAccount_Should_Return_Error()
    {
        const int accountNumber = 12345;
        var account = new Account("John Doe", "12345678900", 55555);
        CancellationToken cancellationToken = default;
        var command = new GetAccountByNumberCommand(accountNumber, cancellationToken);
        
        await _repository.AddAsync(account, cancellationToken);
        await _repository.Commit(cancellationToken);
        await _repository.DetachEntity(account);
        
        var result = await _handler.HandleAsync(command);
        
        Assert.IsNotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
    }
}