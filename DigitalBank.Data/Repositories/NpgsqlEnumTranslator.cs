using Npgsql;

namespace DigitalBank.Data.Repositories;

public class NpgsqlEnumTranslator : INpgsqlNameTranslator
{
    public string TranslateMemberName(string clrName)
    {
        return clrName.ToUpper();
    }

    public string TranslateTypeName(string clrName)
    {
        return clrName;
    }
}
