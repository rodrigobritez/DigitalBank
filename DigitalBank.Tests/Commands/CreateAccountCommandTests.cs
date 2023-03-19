using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class CreateAccountCommandTests
{
  [TestMethod]
  public void Constructor_ShouldSetAccountAndCancellationToken()
  {
    // Arrange
    Account account = new Account();
    CancellationToken cancellationToken = new CancellationToken();

    // Act
    CreateAccountCommand createAccountCommand = new CreateAccountCommand(account, cancellationToken);

    // Assert
    Assert.AreEqual(account, createAccountCommand.Account);
    Assert.AreEqual(cancellationToken, createAccountCommand.CancellationToken);
  }
}