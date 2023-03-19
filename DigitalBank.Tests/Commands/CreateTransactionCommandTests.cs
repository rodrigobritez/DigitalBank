using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class CreateTransactionCommandTests
{
  [TestMethod]
  public void Constructor_ShouldSetTransactionAccountNumberAndCancellationToken()
  {
    // Arrange
    Transaction transaction = new Transaction(100.00m, ETransactionType.DEPOSIT);
    int accountNumber = 12345;
    CancellationToken cancellationToken = new CancellationToken();

    // Act
    CreateTransactionCommand createTransactionCommand = new CreateTransactionCommand(transaction, accountNumber, cancellationToken);

    // Assert
    Assert.AreEqual(transaction, createTransactionCommand.Transaction);
    Assert.AreEqual(accountNumber, createTransactionCommand.AccountNumber);
    Assert.AreEqual(cancellationToken, createTransactionCommand.CancellationToken);
  }
}