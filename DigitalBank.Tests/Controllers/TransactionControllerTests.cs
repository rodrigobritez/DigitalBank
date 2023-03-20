using DigitalBank.API.Controllers;
using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using DigitalBank.Shared.Constants.Enums;
using DigitalBank.Tests.Mocks.Handlers;
namespace DigitalBank.Tests.Controllers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TransactionControllerTests
{
  private ITransactionHandler _handler;
  private TransactionController _controller;

  [TestInitialize]
  public void Setup()
  {
    _handler = new FakeTransactionHandler();
    _controller = new TransactionController();
  }
  
  [TestMethod]
  public async Task CreateAccount_Success_ShouldReturnOk()
  {
    const int accountNumber = 12345;
    var dto = new CreateTransactionDTO(500, ETransactionType.DEPOSIT);

    var result = await _controller.CreateAccount(dto, accountNumber, _handler, CancellationToken.None);

    Assert.IsNotNull(result.Result);
    Assert.AreEqual(dto.Amount, result.Result.Account!.Balance);
    Assert.IsTrue(result.Status == ECommandResultStatus.SUCCESS);
  }
  
  [TestMethod]
  public async Task CreateAccount_Error_ShouldReturnOk()
  {
    const int accountNumber = 55555;
    var dto = new CreateTransactionDTO(500, ETransactionType.DEPOSIT);

    var result = await _controller.CreateAccount(dto, accountNumber, _handler, CancellationToken.None);

    Assert.IsNotNull(result);
    Assert.IsTrue(result.Status == ECommandResultStatus.ERROR);
  }
}