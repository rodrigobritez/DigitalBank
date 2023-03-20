using DigitalBank.GraphQL.Types;

namespace DigitalBank.Tests.Types;

[TestClass]
public class DeleteAccountTypeTests
{
  [TestMethod]
  [TestCategory("Graphql-Type")]
  public void DeleteAccountType_HasCorrectFields()
  {
    var deleteAccountType = new DeleteAccountType();
    
    var accountNumberField = deleteAccountType.Fields.FirstOrDefault(f => f.Name == "AccountNumber");
    
    Assert.IsNotNull(accountNumberField);
    Assert.AreEqual("The number of account", accountNumberField.Description);
    
  }
}