namespace DigitalBank.Domain.Interfaces.Commands;

public interface ICommand
{
  CancellationToken CancellationToken { get; }
}