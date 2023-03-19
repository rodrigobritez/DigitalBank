using DigitalBank.Domain.Entities;
using DigitalBank.GraphQL.Types;
using GraphQL.Types;

namespace DigitalBank.Tests.Types;


[TestClass]
public class AccountTransactionTypeTests
{
  [TestMethod]
  [TestCategory("Graphql-Type")]
  public void AccountTransactionType_HasCorrectFields()
  {
    var accountTransactionType = new AccountTransactionType();

    var accountNumberField = accountTransactionType.Fields.FirstOrDefault(f => f.Name == "AccountNumber");
    
    Assert.IsNotNull(accountNumberField);
    Assert.AreEqual("The number of account", accountNumberField.Description);
  }
}