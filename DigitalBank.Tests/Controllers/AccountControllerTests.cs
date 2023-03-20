using DigitalBank.API.Controllers;
using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.Tests.Mocks.Handlers;

namespace DigitalBank.Tests.Controllers;

[TestClass]
public class AccountControllerTests
{
  private IAccountHandler _handler;
  private AccountController _controller;

  [TestInitialize]
  public void Setup()
  {
    _handler = new FakeAccountHandler();
    _controller = new AccountController();
  }

  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task GetByAccountNumber_Success_ReturnsAccount()
  {
    const int accountNumber = 12345;
    CancellationToken cancellationToken = default;
    var expectedAccount = new Account("Rodrigo", "10358565901", accountNumber);

    var result = await _controller.GetByAccountNumber(accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result.Result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    Assert.AreEqual(accountNumber, result.Result.AccountNumber);
    Assert.AreEqual(expectedAccount.DocumentNumber, result.Result.DocumentNumber);
    Assert.AreEqual(expectedAccount.Name, result.Result.Name);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task GetByAccountNumber_Error_ReturnsAccount()
  {
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;

    var result = await _controller.GetByAccountNumber(accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task UpdateAccount_Success_ReturnsAccount()
  {
    const int accountNumber = 12345;
    var accountDto = new UpdateAccountDTO { Name = "New Name", DocumentNumber = "11111111111" };
    CancellationToken cancellationToken = default;

    var result = await _controller.UpdateAccount(accountDto, accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result.Result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    Assert.AreEqual(accountNumber, result.Result.AccountNumber);
    Assert.AreEqual(accountDto.DocumentNumber, result.Result.DocumentNumber);
    Assert.AreEqual(accountDto.Name, result.Result.Name);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task UpdateAccount_Error_ReturnsAccount()
  {
    const int accountNumber = 55555;
    var accountDto = new UpdateAccountDTO { Name = "New Name", DocumentNumber = "11111111111" };
    CancellationToken cancellationToken = default;

    var result = await _controller.UpdateAccount(accountDto, accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task CreateAccount_Success_ReturnsAccount()
  {
    const string name = "Test Name";
    const string documentNumber = "12345678901";
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;
    var expectedAccount = new Account(name, documentNumber, accountNumber);
    
    var result = await _controller.CreateAccount(name, documentNumber, accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result.Result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
    Assert.AreEqual(expectedAccount.AccountNumber, result.Result.AccountNumber);
    Assert.AreEqual(expectedAccount.DocumentNumber, result.Result.DocumentNumber);
    Assert.AreEqual(expectedAccount.Name, result.Result.Name);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task DeleteAccount_Success()
  {
    const int accountNumber = 12345;
    CancellationToken cancellationToken = default;

    var result = await _controller.DeleteAccount(accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
  }
  
  [TestMethod]
  [TestCategory("API-Controller")]
  public async Task DeleteAccount_Error()
  {
    const int accountNumber = 55555;
    CancellationToken cancellationToken = default;

    var result = await _controller.DeleteAccount(accountNumber, _handler, cancellationToken);
    
    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
  }
}