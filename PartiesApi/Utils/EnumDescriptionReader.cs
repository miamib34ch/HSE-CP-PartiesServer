using System.ComponentModel;

namespace PartiesApi.Utils;

public static class EnumDescriptionReader
{
    public static string GetEnumDescription(Enum? value)
    {
        if (value == null)
            return string.Empty;

        return value.GetType()
            .GetField(value.ToString())
            ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() is not DescriptionAttribute attribute ? value.ToString() : attribute.Description;
    }
}