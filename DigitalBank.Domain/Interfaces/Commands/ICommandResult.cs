using System.ComponentModel.DataAnnotations;
using DigitalBank.Domain.Entities.Base;

namespace DigitalBank.Domain.Interfaces.Commands;

public enum ECommandResultStatus
{
  [Display(Name = "SUCCESS", Description = "The command was executed successfully")]
  SUCCESS,

  [Display(Name = "ERROR", Description = "The command was not executed successfully")]
  ERROR,

  [Display(Name = "ALERT", Description = "The command was executed successfully but there are some alerts")]
  ALERT
}

public interface ICommandResult
{
  ECommandResultStatus Status { get; set; }
  string Message { get; set; }
  string ErrorCode { get; set; }

  ICommandResult<TResult> ToCommandResult<TResult>()
    where TResult : BaseEntity;
  ICommandResult<IEnumerable<TResult>> ToEnumerableCommandResult<TResult>()
    where TResult : BaseEntity;
}

public interface ICommandResult<TResult> : ICommandResult
{
  TResult Result { get; set; }
}