using DigitalBank.Domain.Handlers;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Tests.Handler;

[TestClass]
public class BaseHandlerTests
{
  [TestMethod]
  [TestCategory("Domain-Handler")]
  public void HandleErrors_ShouldReturnDefaultError()
  {
    var baseHandler = new BaseHandler();
    var exception = new Exception("Test Exception");

    var result = baseHandler.HandleErrors(exception);

    Assert.IsNotNull(result);
    Assert.AreEqual(ECommandResultStatus.ERROR, result.Status);
    Assert.AreEqual("Exception: Test Exception", result.Message);
    Assert.AreEqual("Internal Server Error", result.ErrorCode);
  }
}