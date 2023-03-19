using DigitalBank.Domain.Commands;

namespace DigitalBank.Tests.Commands;

[TestClass]
public class GetByIdCommandTests
{
  [TestMethod]
  public void Constructor_ShouldSetIdAndCancellationToken()
  {
    // Arrange
    Guid id = Guid.NewGuid();
    CancellationToken cancellationToken = new CancellationToken();

    // Act
    GetByIdCommand getByIdCommand = new GetByIdCommand(id, cancellationToken);

    // Assert
    Assert.AreEqual(id, getByIdCommand.Id);
    Assert.AreEqual(cancellationToken, getByIdCommand.CancellationToken);
  }
}