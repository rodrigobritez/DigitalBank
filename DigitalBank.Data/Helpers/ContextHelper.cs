using DigitalBank.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;

namespace DigitalBank.Data.Helpers;

public static class ContextHelper
{
  public static void ConfigureEnum<TEnum>(INpgsqlTypeMapper? mapper = null, ModelBuilder? builder = null, string? pgName = null)
    where TEnum : struct, Enum
  {
    if (mapper != null) mapper.MapNpgsqlEnum<TEnum>(pgName);
    if (builder != null) builder.ApplyEnumConfiguration<TEnum>(pgName);
  }

  public static INpgsqlTypeMapper MapNpgsqlEnums(this INpgsqlTypeMapper source, Action<INpgsqlTypeMapper?, ModelBuilder?> enumConfiguration)
  {
    enumConfiguration(source, null);
    return source;
  }

  public static ModelBuilder ApplyEnumsConfiguration(this ModelBuilder source, Action<INpgsqlTypeMapper?, ModelBuilder?> enumConfiguration)
  {
    enumConfiguration(null, source);
    return source;
  }

  private static void MapNpgsqlEnum<TEnum>(this INpgsqlTypeMapper source, string? pgName = null)
    where TEnum : struct, Enum
  {
    source.MapEnum<TEnum>(pgName ?? GetEnumName<TEnum>(), new NpgsqlEnumTranslator());
  }

  private static void ApplyEnumConfiguration<TEnum>(this ModelBuilder source, string? pgName = null)
    where TEnum : struct, Enum
  {
    source.HasPostgresEnum<TEnum>(null, pgName ?? GetEnumName<TEnum>(), new NpgsqlEnumTranslator());
  }

  private static string GetEnumName<TEnum>()
    where TEnum : struct, Enum
  {
    return string.Concat(
      typeof(TEnum).Name.Select(
        (x, i) => (i > 0 && char.IsUpper(x) ? "_" : "") + x.ToString().ToLower()
      )
    );
  }
}