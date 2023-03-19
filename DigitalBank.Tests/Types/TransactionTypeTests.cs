using DigitalBank.GraphQL.Types;
using GraphQL.Types;

namespace DigitalBank.Tests.Types;

[TestClass]
public class TransactionTypeTests
{
  [TestMethod]
  [TestCategory("Graphql-Type")]
  public void TransactionType_HasCorrectFields()
  {
    var transactionType = new TransactionType();
    
    var amountField = transactionType.Fields.FirstOrDefault(f => f.Name == "Amount");
    var typeField = transactionType.Fields.FirstOrDefault(f => f.Name == "Type");
    var accountIdField = transactionType.Fields.FirstOrDefault(f => f.Name == "AccountId");
    
    Assert.IsNotNull(amountField);
    Assert.AreEqual("The amount of transaction", amountField.Description);

    Assert.IsNotNull(typeField);
    Assert.AreEqual("The type of transaction", typeField.Description);

    Assert.IsNotNull(accountIdField);
    Assert.AreEqual("The account id of transaction", accountIdField.Description);
  }
}