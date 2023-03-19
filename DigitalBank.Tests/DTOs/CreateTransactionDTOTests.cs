using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Tests.DTOs;

[TestClass]
public class CreateTransactionDTOTest
{
  [TestMethod]
  [TestCategory("Domain-Dto")]
  public void CreateTransactionDTO_ShouldReturnSameObject()
  {
    var dto = new CreateTransactionDTO
    {
      Amount = 100.00m,
      Type = ETransactionType.DEPOSIT
    };

    var result = new Transaction(dto.Amount, dto.Type);

    Assert.AreEqual(dto.Amount, result.Amount);
    Assert.AreEqual(dto.Type, result.Type);
  }
}