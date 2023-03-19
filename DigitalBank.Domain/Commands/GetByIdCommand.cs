using DigitalBank.Domain.Interfaces.Commands;

namespace DigitalBank.Domain.Commands;

public class GetByIdCommand : ICommand
{
  public GetByIdCommand(Guid id, CancellationToken cancellationToken)
  {
    CancellationToken = cancellationToken;
    Id = id;
  }

  public Guid Id { get; set; }
  public CancellationToken CancellationToken { get; set; }
}