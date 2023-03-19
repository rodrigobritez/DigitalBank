using DigitalBank.Domain.Commands;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class GetAccountByNumberCommandTests
{
  [TestMethod]
  [TestCategory("Domain-Command")]
  public void Constructor_ShouldSetAccountNumberAndCancellationToken()
  {
    const int accountNumber = 12345;
    var cancellationToken = new CancellationToken();

    var getAccountByNumberCommand = new GetAccountByNumberCommand(accountNumber, cancellationToken);
    
    Assert.AreEqual(accountNumber, getAccountByNumberCommand.AccountNumber);
    Assert.AreEqual(cancellationToken, getAccountByNumberCommand.CancellationToken);
  }
}