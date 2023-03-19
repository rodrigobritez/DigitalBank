using DigitalBank.Domain.Entities;

namespace DigitalBank.Domain.DTOs;

public class UpdateAccountDTO
{
  public string Name { get; set; }
  public string DocumentNumber { get; set; }
}