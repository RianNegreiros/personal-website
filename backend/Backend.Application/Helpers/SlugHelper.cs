using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Application.Helpers;

public static class SlugHelper
{
    public static string RemoveAccents(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        text = text.Normalize(NormalizationForm.FormD);
        char[] chars = text
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
            != UnicodeCategory.NonSpacingMark).ToArray();

        return new string(chars).Normalize(NormalizationForm.FormC);
    }

    public static string Slugify(this string phrase)
    {
    // Remove all accents and make the string lower case.  
    string output = phrase.RemoveAccents().ToLower();

    // Remove all special characters from the string, including underscores.  
    output = Regex.Replace(output, @"[^A-Za-z0-9\s\-_]", "");

    // Replace all spaces, underscores, and hyphens with a single hyphen.  
    output = Regex.Replace(output, @"[\s\-_]+", "-");

    // Remove leading and trailing hyphens.
    output = output.Trim('-');

    // Return the slug.  
    return output;
    }
}
