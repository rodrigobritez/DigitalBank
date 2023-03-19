namespace DigitalBank.Domain.Interfaces.Commands;

public interface ICommandHandler { }

public interface ICommandHandler<in TCommand, TResultData> : ICommandHandler
  where TCommand : ICommand
{
  Task<ICommandResult<TResultData>> HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand> : ICommandHandler
  where TCommand : ICommand
{
  Task<ICommandResult> HandleAsync(TCommand command);
}