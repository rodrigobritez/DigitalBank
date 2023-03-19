using DigitalBank.GraphQL.Types;
using GraphQL.Types;

namespace DigitalBank.Tests.Types;

[TestClass]
public class AccountTypeTests
{
  [TestMethod]
  [TestCategory("Graphql-Type")]
  public void AccountType_HasCorrectFields()
  {
    var accountType = new AccountType();
    
    var nameField = accountType.Fields.FirstOrDefault(f => f.Name == "Name");
    var balanceField = accountType.Fields.FirstOrDefault(f => f.Name == "Balance");
    var accountNumberField = accountType.Fields.FirstOrDefault(f => f.Name == "AccountNumber");
    var documentNumberField = accountType.Fields.FirstOrDefault(f => f.Name == "DocumentNumber");
    var transactionsField = accountType.Fields.FirstOrDefault(f => f.Name == "Transactions");

    Assert.IsNotNull(nameField);
    Assert.AreEqual("The name of account", nameField.Description);

    Assert.IsNotNull(balanceField);
    Assert.AreEqual("The balance of account", balanceField.Description);

    Assert.IsNotNull(accountNumberField);
    Assert.AreEqual("The number of account", accountNumberField.Description);

    Assert.IsNotNull(documentNumberField);
    Assert.AreEqual("The document number of account", documentNumberField.Description);

    Assert.IsNotNull(transactionsField);
    Assert.AreEqual("The transactions of account", transactionsField.Description);
    Assert.IsFalse(transactionsField.Arguments.Any());
  }
}