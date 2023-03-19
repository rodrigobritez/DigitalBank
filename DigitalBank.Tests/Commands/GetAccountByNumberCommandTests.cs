using DigitalBank.Domain.Commands;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class GetAccountByNumberCommandTests
{
  [TestMethod]
  public void Constructor_ShouldSetAccountNumberAndCancellationToken()
  {
    // Arrange
    int accountNumber = 12345;
    CancellationToken cancellationToken = new CancellationToken();

    // Act
    GetAccountByNumberCommand getAccountByNumberCommand = new GetAccountByNumberCommand(accountNumber, cancellationToken);

    // Assert
    Assert.AreEqual(accountNumber, getAccountByNumberCommand.AccountNumber);
    Assert.AreEqual(cancellationToken, getAccountByNumberCommand.CancellationToken);
  }
}