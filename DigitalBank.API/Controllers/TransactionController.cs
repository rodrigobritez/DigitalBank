using DigitalBank.Domain.Commands;
using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.Domain.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DigitalBank.API.Controllers;

[Route("/transaction")]
public class TransactionController : ControllerBase
{
  
  [HttpPost("Create")]
  public async Task<ICommandResult<Transaction>> CreateAccount(
    [FromQuery] CreateTransactionDTO transactionDto, [BindRequired] int accountNumber,
    [FromServices] ITransactionHandler handler,
    CancellationToken cancellationToken = default)
  {
    var transaction = new Transaction(transactionDto.Amount, transactionDto.Type);
    var command = new CreateTransactionCommand(transaction, accountNumber, cancellationToken);
    var result = await handler.HandleAsync(command);
    return result;
  }
}