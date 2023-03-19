using DigitalBank.Domain.Commands;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class GetByIdCommandTests
{
  [TestMethod]
  [TestCategory("Domain-Command")]
  public void Constructor_ShouldSetIdAndCancellationToken()
  {
    var id = Guid.NewGuid();
    var cancellationToken = new CancellationToken();
    
    var getByIdCommand = new GetByIdCommand(id, cancellationToken);
    
    Assert.AreEqual(id, getByIdCommand.Id);
    Assert.AreEqual(cancellationToken, getByIdCommand.CancellationToken);
  }
}