

using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalBank.Tests.Entities;

  [TestClass]
  public class TransactionTests
  {
    [TestMethod]
    public void Constructor_ShouldSetProperties()
    {
      decimal amount = 100;
      ETransactionType type = ETransactionType.DEPOSIT;

      Transaction transaction = new Transaction(amount, type);

      Assert.AreEqual(amount, transaction.Amount);
      Assert.AreEqual(type, transaction.Type);
    }
    
    [TestMethod]
    public void Id_ShouldBeGenerated()
    {
      // Arrange
      Transaction transaction1 = new Transaction();
      Transaction transaction2 = new Transaction();

      // Assert
      Assert.AreNotEqual(transaction1.Id, transaction2.Id);
    }
    
    [TestMethod]
    public void Account_ShouldBeSet()
    {
      // Arrange
      Account account = new Account();
      Transaction transaction = new Transaction();

      // Act
      transaction.Account = account;

      // Assert
      Assert.AreEqual(account, transaction.Account);
    }
  }


