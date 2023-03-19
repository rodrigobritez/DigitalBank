using DigitalBank.Domain.Entities;

namespace DigitalBank.Tests.Entities;

[TestClass]
public class AccountTests
{
  private Account account;

  [TestInitialize]
  public void Initialize()
  {
    account = new Account("John", "123456789", 1);
  }

  [TestMethod]
  public void TestDeposit()
  {
    // Arrange
    decimal amount = 100;

    // Act
    account.Deposit(amount);

    // Assert
    Assert.AreEqual(amount, account.Balance);
  }

  [TestMethod]
  public void TestWithDraw()
  {
    // Arrange
    decimal initialBalance = 500;
    decimal withdrawAmount = 100;
    account.Deposit(initialBalance);

    // Act
    account.WithDraw(withdrawAmount);

    // Assert
    Assert.AreEqual(initialBalance - withdrawAmount, account.Balance);
  }

  [TestMethod]
  public void TestAccountNumber()
  {
    // Assert
    Assert.AreEqual(1, account.AccountNumber);
  }

  [TestMethod]
  public void TestName()
  {
    // Assert
    Assert.AreEqual("John", account.Name);
  }

  [TestMethod]
  public void TestDocumentNumber()
  {
    // Assert
    Assert.AreEqual("123456789", account.DocumentNumber);
  }
}
