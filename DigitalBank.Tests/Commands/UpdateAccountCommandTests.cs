using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class UpdateAccountCommandTests
{
  [TestMethod]
  public void Constructor_ShouldSetAccountAndCancellationToken()
  {
    // Arrange
    Account account = new Account();
    CancellationToken cancellationToken = new CancellationToken();

    // Act
    UpdateAccountCommand updateAccountCommand = new UpdateAccountCommand(account, cancellationToken);

    // Assert
    Assert.AreEqual(account, updateAccountCommand.Account);
    Assert.AreEqual(cancellationToken, updateAccountCommand.CancellationToken);
  }
}