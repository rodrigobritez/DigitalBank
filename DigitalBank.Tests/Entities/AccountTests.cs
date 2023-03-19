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
  [TestCategory("Domain-Entity")]
  public void TestDeposit()
  {
    const decimal amount = 100;
    account.Deposit(amount);

    Assert.AreEqual(amount, account.Balance);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void TestWithDraw()
  {
    decimal initialBalance = 500;
    decimal withdrawAmount = 100;
    account.Deposit(initialBalance);
    
    account.WithDraw(withdrawAmount);
    
    Assert.AreEqual(initialBalance - withdrawAmount, account.Balance);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void TestAccountNumber()
  {
    Assert.AreEqual(1, account.AccountNumber);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void TestName()
  {
    Assert.AreEqual("John", account.Name);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void TestDocumentNumber()
  {
    Assert.AreEqual("123456789", account.DocumentNumber);
  }
}
