

using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalBank.Tests.Entities;

  [TestClass]
  public class TransactionTests
  {
    [TestMethod]
    [TestCategory("Domain-Entity")]
    public void Constructor_ShouldSetProperties()
    {
      const decimal amount = 100;
      const ETransactionType type = ETransactionType.DEPOSIT;

      var transaction = new Transaction(amount, type);

      Assert.AreEqual(amount, transaction.Amount);
      Assert.AreEqual(type, transaction.Type);
    }
    
    [TestMethod]
    [TestCategory("Domain-Entity")]
    public void Id_ShouldBeGenerated()
    {
      var transaction1 = new Transaction();
      var transaction2 = new Transaction();
      
      Assert.AreNotEqual(transaction1.Id, transaction2.Id);
    }
    
    [TestMethod]
    [TestCategory("Domain-Entity")]
    public void Account_ShouldBeSet()
    {
      var account = new Account();
      var transaction = new Transaction();

      transaction.Account = account;

      Assert.AreEqual(account, transaction.Account);
    }
  }


