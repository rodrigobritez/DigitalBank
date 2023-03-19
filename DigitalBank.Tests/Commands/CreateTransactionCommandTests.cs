using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class CreateTransactionCommandTests
{
  [TestMethod]
  [TestCategory("Domain-Command")]
  public void Constructor_ShouldSetTransactionAccountNumberAndCancellationToken()
  {
    var transaction = new Transaction(100.00m, ETransactionType.DEPOSIT);
    const int accountNumber = 12345;
    var cancellationToken = new CancellationToken();
    
    var createTransactionCommand = new CreateTransactionCommand(transaction, accountNumber, cancellationToken);
    
    Assert.AreEqual(transaction, createTransactionCommand.Transaction);
    Assert.AreEqual(accountNumber, createTransactionCommand.AccountNumber);
    Assert.AreEqual(cancellationToken, createTransactionCommand.CancellationToken);
  }
}