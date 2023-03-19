using DigitalBank.Domain.Entities.Base;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Commands;

public class CommandResult : ICommandResult
{
  public ECommandResultStatus Status { get; set; }
  public string Message { get; set; }
  public string ErrorCode { get; set; }
  
  public CommandResult(
    ECommandResultStatus status,
    string message,
    string errorCode = null!)
  {
    Status = status;
    Message = message;
    ErrorCode = errorCode;
  }

  public ICommandResult<TResult> ToCommandResult<TResult>()
    where TResult : BaseEntity
  {
    return new CommandResult<TResult>(Status, Message, null!, ErrorCode);
  }

  public ICommandResult<IEnumerable<TResult>> ToEnumerableCommandResult<TResult>()
    where TResult : BaseEntity
  {
    return new CommandResult<IEnumerable<TResult>>(Status, Message, null!, ErrorCode);
  }
}

public class CommandResult<TResult> : CommandResult, ICommandResult<TResult>
{
  public TResult Result { get; set; }

  public CommandResult(
    ECommandResultStatus status,
    string message,
    TResult result,
    string errorCode = null!)
    : base(status, message, errorCode)
  {
    Result = result;
  }
}