using DigitalBank.Data.Helpers;
using DigitalBank.Data.Mappings.Base;
using DigitalBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalBank.Data.Mappings;

public class AccountMapping : BaseEntityMap<Account>
{
  public override void Configure(EntityTypeBuilder<Account> builder)
  {
    base.Configure(builder);

    builder.ToTable("reg_accounts");

    // Base properties
    builder.MapBaseEntityDefaultProps("id_account");

    // Entity properties
    builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(50)").IsRequired();
    builder.Property(x => x.DocumentNumber).HasColumnName("document_number").HasColumnType("varchar(13)").IsRequired();
    builder.Property(x => x.Balance).HasColumnName("balance").HasColumnType("int").IsRequired().HasDefaultValue(0);
    builder.Property(x => x.AccountNumber).HasColumnName("account_number").HasColumnType("int").IsRequired();

    // Relationships
    builder.HasMany(x => x.Transactions)
      .WithOne(x => x.Account)
      .HasForeignKey(x => x.AccountId);
    
    // Constraints and indexes
    builder.HasIndex(x => new { x.AccountNumber }).IsUnique().HasFilter("deleted = false");
  }
}