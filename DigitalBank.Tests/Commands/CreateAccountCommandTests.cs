using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class CreateAccountCommandTests
{
  [TestMethod]
  [TestCategory("Domain-Command")]
  public void Constructor_ShouldSetAccountAndCancellationToken()
  {
    var account = new Account();
    var cancellationToken = new CancellationToken();
    
    var createAccountCommand = new CreateAccountCommand(account, cancellationToken);
    
    Assert.AreEqual(account, createAccountCommand.Account);
    Assert.AreEqual(cancellationToken, createAccountCommand.CancellationToken);
  }
}