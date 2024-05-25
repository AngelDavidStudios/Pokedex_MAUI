using System.Globalization;
using Pokedex_MAUI.Enums;

namespace Pokedex_MAUI.Converters;

public class ConverterSortToDescriptionSort: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        SortEnum type = SortEnum.Ascending;

        if (!(value is SortEnum))
        {
            if (!(value is string))
                return null;

            if (!Enum.TryParse((string)value, out type))
                type = SortEnum.Ascending;
        }
        else
            type = (SortEnum)value;

        switch (type)
        {
            case SortEnum.Ascending:
                return "Smallest number first";
            case SortEnum.Descending:
                return "Highest number first";
            case SortEnum.AlphabeticalAscending:
                return "A-Z";
            case SortEnum.AlphabeticalDescending:
                return "Z-A";
            default:
                return "";
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}