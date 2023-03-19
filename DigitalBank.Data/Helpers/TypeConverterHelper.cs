using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalBank.Data.Helpers;

public class TypeConverterHelper
{
  public class DateTimeConverter : ValueConverter<DateTime, DateTime>
  {
    public DateTimeConverter()
      : base(
        v => v,
        v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
  }
}