using DigitalBank.GraphQL.Types;
using DigitalBank.Shared.Constants.Enums;
using GraphQL.Types;

namespace DigitalBank.Tests.Types;

[TestClass]
public class CreateTransactionTypeTests
{
  [TestMethod]
  [TestCategory("Graphql-Type")]
  public void CreateTransactionType_HasCorrectFields()
  {
    var createTransactionType = new CreateTransactionType();

    var amountField = createTransactionType.Fields.FirstOrDefault(f => f.Name == "Amount");
    var typeField = createTransactionType.Fields.FirstOrDefault(f => f.Name == "Type");
    var accountField = createTransactionType.Fields.FirstOrDefault(f => f.Name == "Account");
    
    Assert.IsNotNull(amountField);
    Assert.AreEqual("The amount of transaction", amountField.Description);


    Assert.IsNotNull(typeField);
    Assert.AreEqual("The type of transaction", typeField.Description);

    Assert.IsNotNull(accountField);
    Assert.AreEqual("The account number of transaction", accountField.Description);
  }
}