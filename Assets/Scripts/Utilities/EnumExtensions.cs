using System.Globalization;
using System.Text.RegularExpressions;

public static class EnumExtensions
{
    public static string ToFormattedString(this ItemName itemName)
    {
        string result = Regex.Replace(itemName.ToString(), "_", " ");
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());
    }
}
