namespace DigitalBank.Domain.Interfaces.Repositories;

public interface IBaseRepository
{
  public Task Commit(CancellationToken cancellationToken);
}