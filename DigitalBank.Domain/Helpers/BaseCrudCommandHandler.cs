using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Helpers;

public abstract class BaseCrudCommandHandler
{
  protected abstract ICommandResult HandleErrors(Exception exception);
}