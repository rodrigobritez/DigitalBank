using DigitalBank.Domain.Entities.Base;

namespace DigitalBank.Domain.Entities;

public class Account : BaseEntity
{
  public Account()
  {
  }

  public Account(string name, string documentNumber, int accountNumber)
  {
    Name = name;
    DocumentNumber = documentNumber;
    AccountNumber = accountNumber;
  }

  public string Name { get; set; }
  public string DocumentNumber { get; set; }
  public decimal Balance { get; private set; }
  public int AccountNumber { get; set; }
  public virtual IEnumerable<Transaction>? Transactions { get; set; }
  
  /// <summary>
  ///   The method is responsible for depositing money in the account.
  ///   <param name="amount">The amount for deposit.</param>
  /// </summary>
  public void Deposit(decimal amount)
  {
    Balance += amount;
  }
  
  /// <summary>
  ///   The method is responsible for withdrawing money from the account
  ///   <param name="amount">The amount for withdraw.</param>
  /// </summary>
  public void WithDraw(decimal amount)
  {
    Balance -= amount;
  }
}