using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class UpdateAccountCommandTests
{
  [TestMethod]
  [TestCategory("Domain-Command")]
  public void Constructor_ShouldSetAccountAndCancellationToken()
  {
    var account = new Account();
    var cancellationToken = new CancellationToken();
    
    var updateAccountCommand = new UpdateAccountCommand(account, cancellationToken);
    
    Assert.AreEqual(account, updateAccountCommand.Account);
    Assert.AreEqual(cancellationToken, updateAccountCommand.CancellationToken);
  }
}