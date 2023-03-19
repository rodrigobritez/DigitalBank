namespace DigitalBank.Domain.Entities.Base;

public class BaseEntity
{
  public Guid Id { get; set; } = Guid.NewGuid();

  public bool Deleted { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public DateTime? DeletedAt { get; set; }
}