using DigitalBank.Domain.Entities;
using DigitalBank.GraphQL.Types;
using GraphQL.Types;

namespace DigitalBank.Tests.Types;


[TestClass]
public class AccountTransactionTypeTests
{
  [TestMethod]
  public void AccountTransactionType_HasCorrectFields()
  {
    // Arrange
    var accountTransactionType = new AccountTransactionType();

    // Act
    var accountNumberField = accountTransactionType.Fields.FirstOrDefault(f => f.Name == "AccountNumber");

    // Assert
    Assert.IsNotNull(accountNumberField);
    Assert.AreEqual("The number of account", accountNumberField.Description);
  }
}