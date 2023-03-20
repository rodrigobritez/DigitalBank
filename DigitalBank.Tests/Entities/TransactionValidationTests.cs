using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Validators;
using DigitalBank.Shared.Constants.Enums;

namespace DigitalBank.Tests.Entities;

[TestClass]
public class TransactionValidationTests
{
  private readonly Account _account = new()
  {
    Id = new Guid("0ba9fb20-e988-4d9f-ba70-5b9b4522624d"),
    Name = "Test",
    AccountNumber = 12345,
    DocumentNumber = "12345678901"
  };

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateTransaction_Deposit_Valid()
  {
    var transaction = new Transaction()
    {
      AccountId = _account.Id,
      Amount = 100,
      Type = ETransactionType.DEPOSIT
    };
    
    var result =  Validator.ValidateAsync<TransactionValidator, Transaction>(transaction, "Create", CancellationToken.None);
    Assert.IsTrue(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateTransaction_Withdraw_Valid()
  {
    var transaction = new Transaction()
    {
      AccountId = _account.Id,
      Amount = 100,
      Type = ETransactionType.WITHDRAW
    };
    
    var result =  Validator.ValidateAsync<TransactionValidator, Transaction>(transaction, "Create", CancellationToken.None);
    Assert.IsTrue(result.Result.IsValid);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateTransaction_Without_Amount_Invalid()
  {
    var transaction = new Transaction()
    {
      AccountId = _account.Id,
      Amount = 0,
      Type = ETransactionType.WITHDRAW
    };
    
    var result =  Validator.ValidateAsync<TransactionValidator, Transaction>(transaction, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
  
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateTransaction_Without_Type_Invalid()
  {
    var transaction = new Transaction()
    {
      AccountId = _account.Id,
      Amount = 100
    };
    
    var result =  Validator.ValidateAsync<TransactionValidator, Transaction>(transaction, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreateTransaction_With_Negative_Amount_Invalid()
  {
    var transaction = new Transaction()
    {
      AccountId = _account.Id,
      Amount = -100,
      Type = ETransactionType.WITHDRAW
    };
    
    var result =  Validator.ValidateAsync<TransactionValidator, Transaction>(transaction, "Create", CancellationToken.None);
    Assert.IsFalse(result.Result.IsValid);
  }
}