using System.ComponentModel.DataAnnotations;
using DigitalBank.Domain.Commands;
using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DigitalBank.API.Controllers;

[Route("/account")]
public class AccountController : ControllerBase
{
  [HttpGet("{id:Guid}")]
  public async Task<ICommandResult<Account>> GetById(
    Guid id,
    [FromServices] IAccountHandler handler,
    CancellationToken cancellationToken = default)
  {
    var command = new GetByIdCommand(id, cancellationToken);
    var result = await handler.HandleAsync(command);
    return result;
  }
  
  [HttpGet("{accountNumber:int}")]
  public async Task<ICommandResult<Account>> GetByAccountNumber(
    int accountNumber,
    [FromServices] IAccountHandler handler,
    CancellationToken cancellationToken = default)
  {
    var command = new GetAccountByNumberCommand(accountNumber, cancellationToken);
    var result = await handler.HandleAsync(command);
    return result;
  }
  
  [HttpPatch("Update")]
  public async Task<ICommandResult<Account>> UpdateAccount(
    [FromQuery] UpdateAccountDTO accountDto, [BindRequired] int accountNumber,
    [FromServices] IAccountHandler handler,
    CancellationToken cancellationToken = default)
  {
    var account = new Account(accountDto.Name, accountDto.DocumentNumber, accountNumber);
    var command = new UpdateAccountCommand(account, cancellationToken);
    var result = await handler.HandleAsync(command);
    return result;
  }
  
  [HttpPost("Create")]
  public async Task<ICommandResult<Account>> CreateAccount(
    [FromQuery] [BindRequired] string name, [BindRequired] string documentNumber, [BindRequired] int accountNumber,
    [FromServices] IAccountHandler handler,
    CancellationToken cancellationToken = default)
  {
    var account = new Account(name, documentNumber, accountNumber);
    var command = new CreateAccountCommand(account, cancellationToken);
    var result = await handler.HandleAsync(command);
    return result;
  }
}