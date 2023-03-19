using DigitalBank.Data.Helpers;
using DigitalBank.Data.Mappings.Base;
using DigitalBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalBank.Data.Mappings;

public class TransactionMapping : BaseEntityMap<Transaction>
{
  public override void Configure(EntityTypeBuilder<Transaction> builder)
  {
    base.Configure(builder);

    builder.ToTable("reg_transactions");

    // Base properties
    builder.MapBaseEntityDefaultProps("id_transaction");

    // Entity properties
    builder.Property(x => x.Amount).HasColumnName("amount").HasColumnType("decimal").IsRequired();

    // Relationships
    builder.HasOne(x => x.Account)
      .WithMany(x => x.Transactions)
      .HasForeignKey(x => x.AccountId);
  }
}