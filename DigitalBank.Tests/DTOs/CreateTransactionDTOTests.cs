using DigitalBank.Domain.DTOs;
using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Tests.DTOs;

[TestClass]
public class CreateTransactionDTOTest
{
  [TestMethod]
  public void CreateTransactionDTO_ShouldReturnSameObject()
  {
    // Arrange
    var dto = new CreateTransactionDTO
    {
      Amount = 100.00m,
      Type = ETransactionType.DEPOSIT
    };

    // Act
    var result = new Transaction(dto.Amount, dto.Type);

    // Assert
    Assert.AreEqual(dto.Amount, result.Amount);
    Assert.AreEqual(dto.Type, result.Type);
  }
}