using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Handlers;

public class BaseHandler
{
  public ICommandResult GetDefaultError(Exception exception)
  {
    return new CommandResult(
      ECommandResultStatus.ERROR,
      "Exception: " + exception.Message,
      "Internal Server Error"
    );
  }

  public ICommandResult HandleErrors(Exception exception)
  {
    var result = GetDefaultError(exception);

    return result;
  }
}