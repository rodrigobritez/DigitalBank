using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Validators;

namespace DigitalBank.Tests.Entities;

[TestClass]
public class AccountCreateValidateTests
{
  private readonly Account _account = new()
  {
    Name = "Test",
    AccountNumber = 12345,
    DocumentNumber = "12345678901"
  };

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_Valid()
  {
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsTrue(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_Document_Invalid_Need_Exactly_11_Characters()
  {
    _account.DocumentNumber = "123456789012";
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_AccountNumber_Invalid_Range_Between_1000_And_99999()
  {
    _account.AccountNumber = 123456;
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_Name_Invalid_Maximum_50_Characters()
  {
    _account.Name = "Nam quis nulla. Integer malesuada. In in enim a arc";
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_Name_Invalid_Minimum_3_Characters()
  {
    _account.Name = "RO";
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_Name_Invalid_NotEmpty()
  {
    _account.Name = "";
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_AccountNumber_Invalid_NotEmpty()
  {
    _account.AccountNumber = 0;
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateAccount_Document_Invalid_NotEmpty()
  {
    _account.DocumentNumber = "";
    var result =  Validator.ValidateAsync<AccountValidator, Account>(_account, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
}