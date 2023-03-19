using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;

namespace DigitalBank.Tests.DTOs;

[TestClass]
public class UpdateAccountDTOTest
{
  [TestMethod]
  [TestCategory("Domain-Dto")]
  public void UpdateAccountDTO_ShouldReturnSameObject()
  {
    var dto = new UpdateAccountDTO
    {
      Name = "John Doe",
      DocumentNumber = "123456789"
    };

    var result = new Account(dto.Name, dto.DocumentNumber, 1);

    Assert.AreEqual(dto.Name, result.Name);
    Assert.AreEqual(dto.DocumentNumber, result.DocumentNumber);
  }
}