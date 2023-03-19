using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class CommandResultTests
{
  [TestMethod]
  public void Constructor_ShouldSetProperties()
  {
    // Arrange
    ECommandResultStatus status = ECommandResultStatus.SUCCESS;
    string message = "Command executed successfully";
    string errorCode = "123";

    // Act
    CommandResult commandResult = new CommandResult(status, message, errorCode);

    // Assert
    Assert.AreEqual(status, commandResult.Status);
    Assert.AreEqual(message, commandResult.Message);
    Assert.AreEqual(errorCode, commandResult.ErrorCode);
  }

  [TestMethod]
  public void ConstructorWithType_ShouldSetPropertiesAndResult()
  {
    // Arrange
    ECommandResultStatus status = ECommandResultStatus.SUCCESS;
    string message = "Command executed successfully";
    string errorCode = "123";
    Account account = new Account();

    // Act
    CommandResult<Account> commandResult = new CommandResult<Account>(status, message, account, errorCode);

    // Assert
    Assert.AreEqual(status, commandResult.Status);
    Assert.AreEqual(message, commandResult.Message);
    Assert.AreEqual(errorCode, commandResult.ErrorCode);
    Assert.AreEqual(account, commandResult.Result);
  }
}