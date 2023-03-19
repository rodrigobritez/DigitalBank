using DigitalBank.GraphQL.Types;
using GraphQL.Types;

namespace DigitalBank.Tests.Types;

[TestClass]
public class CreateAccountTypeTests
{
  [TestMethod]
  [TestCategory("Graphql-Type")]
  public void CreateAccountType_HasCorrectFields()
  {
    var createAccountType = new CreateAccountType();

    var nameField = createAccountType.Fields.FirstOrDefault(f => f.Name == "Name");
    var accountNumberField = createAccountType.Fields.FirstOrDefault(f => f.Name == "AccountNumber");
    var documentNumberField = createAccountType.Fields.FirstOrDefault(f => f.Name == "DocumentNumber");
    
    Assert.IsNotNull(nameField);
    Assert.AreEqual("The name of account", nameField.Description);

    Assert.IsNotNull(accountNumberField);
    Assert.AreEqual("The number of account", accountNumberField.Description);

    Assert.IsNotNull(documentNumberField);
    Assert.AreEqual("The document number of account", documentNumberField.Description);
  }
}