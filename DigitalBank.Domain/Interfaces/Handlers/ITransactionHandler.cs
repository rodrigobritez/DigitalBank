using DigitalBank.Domain.Commands;
using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Interfaces.Handlers;

public interface ITransactionHandler : ICommandHandler<CreateTransactionCommand, Transaction>
{
}