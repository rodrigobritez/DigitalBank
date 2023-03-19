using DigitalBank.Data.Helpers;
using DigitalBank.Data.Mappings;
using DigitalBank.Shared.Constants.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.TypeMapping;

namespace DigitalBank.Data.Context;

public class DataContext : DbContext
{
  private static readonly Action<INpgsqlTypeMapper?, ModelBuilder?> EnumConfiguration = (mapper, builder) =>
  {
    ContextHelper.ConfigureEnum<ETransactionType>(mapper, builder);
  };

  // Register enum type on DB
  static DataContext()
  {
    NpgsqlConnection.GlobalTypeMapper.MapNpgsqlEnums(EnumConfiguration);
  }

  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.ApplyEnumsConfiguration(EnumConfiguration);
    modelBuilder.HasDefaultSchema("digital_bank");

    // Application mappings
    modelBuilder.ApplyConfiguration(new AccountMapping());
    modelBuilder.ApplyConfiguration(new TransactionMapping());
    
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder
      .Properties<DateTime>()
      .HaveConversion<TypeConverterHelper.DateTimeConverter>();
  }
}