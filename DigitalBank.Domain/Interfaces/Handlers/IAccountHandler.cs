using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Interfaces.Handlers;

public interface IAccountHandler : ICommandHandler<GetByIdCommand, Account>,
  ICommandHandler<CreateAccountCommand, Account>, ICommandHandler<UpdateAccountCommand, Account>,
  ICommandHandler<GetAccountByNumberCommand, Account>
{
  
}