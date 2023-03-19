using DigitalBank.GraphQL.Types;

namespace DigitalBank.Tests.Types;

[TestClass]
public class UpdateAccountTypeTests
{
  [TestMethod]
  [TestCategory("Graphql-Type")]
  public void UpdateAccountType_HasCorrectFields()
  {
    var updateAccountType = new UpdateAccountType();

    var nameField = updateAccountType.Fields.FirstOrDefault(f => f.Name == "Name");
    var accountNumberField = updateAccountType.Fields.FirstOrDefault(f => f.Name == "AccountNumber");
    var documentNumberField = updateAccountType.Fields.FirstOrDefault(f => f.Name == "DocumentNumber");

    Assert.IsNotNull(nameField);
    Assert.AreEqual("The name of account", nameField.Description);

    Assert.IsNotNull(accountNumberField);
    Assert.AreEqual("The number of account", accountNumberField.Description);

    Assert.IsNotNull(documentNumberField);
    Assert.AreEqual("The document number of account", documentNumberField.Description);
  }
}