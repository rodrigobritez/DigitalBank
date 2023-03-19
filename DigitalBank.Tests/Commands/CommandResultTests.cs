using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class CommandResultTests
{
  [TestMethod]
  [TestCategory("Domain-Command")]
  public void Constructor_ShouldSetProperties()
  {
    const ECommandResultStatus status = ECommandResultStatus.SUCCESS;
    const string message = "Command executed successfully";
    const string errorCode = "123";
    
    var commandResult = new CommandResult(status, message, errorCode);
    
    Assert.AreEqual(status, commandResult.Status);
    Assert.AreEqual(message, commandResult.Message);
    Assert.AreEqual(errorCode, commandResult.ErrorCode);
  }

  [TestMethod]
  [TestCategory("Domain-Command")]
  public void ConstructorWithType_ShouldSetPropertiesAndResult()
  {
    const ECommandResultStatus status = ECommandResultStatus.SUCCESS;
    const string message = "Command executed successfully";
    const string errorCode = "123";
    var account = new Account();
    
    var commandResult = new CommandResult<Account>(status, message, account, errorCode);
    
    Assert.AreEqual(status, commandResult.Status);
    Assert.AreEqual(message, commandResult.Message);
    Assert.AreEqual(errorCode, commandResult.ErrorCode);
    Assert.AreEqual(account, commandResult.Result);
  }
}